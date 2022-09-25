using System.Collections.Concurrent;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using CsvHelper;
using Dasync.Collections;
using DiceClub.Api.Data;
using DiceClub.Api.Data.Cards;
using DiceClub.Database.Context;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScryfallApi.Client;
using ScryfallApi.Client.Models;

namespace DiceClub.Services.Cards;

public class CardService : AbstractBaseService<CardService>
{
    private readonly ScryfallApiClient _scryfallApiClient;
    private readonly MtgDumpDao _mtgDumpDao;
    private readonly MtgCardDao _mtgCardDao;
    private readonly MtgCardLanguageDao _mtgCardLanguageDao;
    private readonly MtgCardColorDao _mtgCardColorDao;
    private readonly MtgCardSetDao _mtgCardSetDao;
    private readonly MtgCardColorRelDao _mtgCardColorRelDao;
    private readonly MtgCardLegalityDao _mtgCardLegalityDao;
    private readonly MtgCardLegalityTypeDao _mtgCardLegalityTypeDao;
    private readonly MtgCardLegalityRelDao _mtgCardLegalityRelDao;
    private readonly string _scryfallBaseUrl = "https://api.scryfall.com/";

    private readonly MtgCardTypeDao _mtgCardTypeDao;
    private readonly MtgCardRarityDao _mtgCardRarityDao;

    public CardService(IEventBusService eventBusService, ILogger<CardService> logger,
        ScryfallApiClient scryfallApiClient, MtgDumpDao mtgDumpDao, MtgCardRarityDao mtgCardRarityDao,
        MtgCardTypeDao mtgCardTypeDao, MtgCardDao mtgCardDao, MtgCardColorDao mtgCardColorDao,
        MtgCardSetDao mtgCardSetDao, MtgCardLanguageDao mtgCardLanguageDao, MtgCardColorRelDao mtgCardColorRelDao,
        MtgCardLegalityDao mtgCardLegalityDao, MtgCardLegalityTypeDao mtgCardLegalityTypeDao,
        MtgCardLegalityRelDao mtgCardLegalityRelDao) : base(eventBusService,
        logger)
    {
        _scryfallApiClient = scryfallApiClient;
        _mtgDumpDao = mtgDumpDao;
        _mtgCardRarityDao = mtgCardRarityDao;
        _mtgCardTypeDao = mtgCardTypeDao;
        _mtgCardDao = mtgCardDao;
        _mtgCardColorDao = mtgCardColorDao;
        _mtgCardSetDao = mtgCardSetDao;
        _mtgCardLanguageDao = mtgCardLanguageDao;
        _mtgCardColorRelDao = mtgCardColorRelDao;
        _mtgCardLegalityDao = mtgCardLegalityDao;
        _mtgCardLegalityTypeDao = mtgCardLegalityTypeDao;
        _mtgCardLegalityRelDao = mtgCardLegalityRelDao;
    }


    public async Task<List<MtgCardEntity>> SearchCards(SearchCardRequest request, Guid userId)
    {
        var colors = new List<Guid>();

        if (request.Colors != null)
        {
            foreach (var c in request.Colors)
            {
                var color = await _mtgCardColorDao.QueryAsSingle(entities => entities.Where(s => s.Name == c));
                if (color != null)
                {
                    colors.Add(color.Id);
                }
            }
        }

        var result = await _mtgCardDao.QueryAsList(entities =>
        {
            if (!string.IsNullOrEmpty(request.Description))
            {
                entities = entities.Where(s => s.OwnerId == userId);

                entities = entities.Where(s =>
                    EF.Functions.ToTsVector("italian", s.Description)
                        .Matches($"{request.Description}:*") ||
                    EF.Functions.ToTsVector("italian", s.ForeignNames)
                        .Matches($"{request.Description}:*") ||
                    EF.Functions.ToTsVector("italian", s.Name)
                        .Matches($"{request.Description}:*")
                    
                );

                if (colors.Any())
                {
                    entities = entities.Where(s => s.Colors.All(k => colors.Contains(k.ColorId)));
                }
            }

            entities = entities
                .Include(s => s.Colors)
                .ThenInclude(s => s.Color)
                .Include(s => s.Language)
                .Include(s => s.Rarity)
                .Include(s => s.Set)
                .Include(s => s.Type)
                .Include(s => s.Legalities);

            if (request.OrderBy != null)
            {
                switch (request.OrderBy)
                {
                    case SearchCardRequestOrderBy.Name:
                        entities = entities.OrderBy(s => s.Name);
                        break;
                    case SearchCardRequestOrderBy.Price:
                        entities = entities.OrderBy(s => s.Price);
                        break;
                    case SearchCardRequestOrderBy.Set:
                        entities = entities.OrderBy(s => s.SetId);
                        break;
                    case SearchCardRequestOrderBy.CreatedDate:
                        entities = entities.OrderBy(s => s.CreateDateTime);
                        break;
                    case SearchCardRequestOrderBy.Rarity:
                        entities = entities.OrderBy(s => s.RarityId);
                        break;
                    case SearchCardRequestOrderBy.CardType:
                        entities = entities.OrderBy(s => s.TypeId);
                        break;
                    case SearchCardRequestOrderBy.Quantity:
                        entities = entities.OrderBy(s => s.Quantity);
                        break;
                }
            }

            entities = entities.AsSplitQuery();
            return entities;
        });


        return result;
    }

    public async Task ImportCsv(string fileName, CardCsvImportType importFormat, Guid userId)
    {
        await PublishEvent(NotificationEventBuilder.Create().Broadcast().Message($"Importing CSV format {importFormat}")
            .Title("Import")
            .Type(NotificationEventType.Information)
            .ToUser(userId)
            .Build());

        switch (importFormat)
        {
            case CardCsvImportType.CardCastle:
                await ImportCardCastleFormat(fileName, userId);
                break;
        }
    }

    public async Task ImportCardCastleFormat(string fileName, Guid userId)
    {
        var records = ParseCsvFormFile<CardCastleRow>(fileName);
        var mtgDumpCards = new ConcurrentBag<Card>();
        using var httpClient = new HttpClient() { BaseAddress = new Uri(_scryfallBaseUrl) };
        await records.ParallelForEachAsync(async (row, index) =>
        {
            Card fetchedCard = null;
            Logger.LogInformation("Fetching card: {CardName} [{Index}/{Count}]", row.CardName, index, records.Count);
            if (!string.IsNullOrEmpty(row.JsonId))
            {
                try
                {
                    var result = await httpClient.GetFromJsonAsync<Card>($"cards/{row.JsonId}");
                    if (result != null)
                    {
                        fetchedCard = result;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error during get card: {CardName} - {Ex}", row.CardName, ex.Message);
                }
            }

            if (fetchedCard == null)
            {
                try
                {
                    //Logger.LogWarning("Dropping card: {Name}, not found in any database", row.CardName);
                    var searchedCards = await _scryfallApiClient.Cards.Search(row.CardName, 1, new SearchOptions
                    {
                        Mode = SearchOptions.RollupMode.Cards,
                        IncludeExtras = true,
                        IncludeMultilingual = true
                    });

                    if (searchedCards.TotalCards > 0)
                    {
                        mtgDumpCards.Add(searchedCards.Data.First());
                    }
                    else
                    {
                        Logger.LogWarning("Dropping card {CardName}, not found in any database", row.CardName);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error during get card: {CardName} = {Ex}", row.CardName, ex.Message);
                }
            }
            else
            {
                mtgDumpCards.Add(fetchedCard);
            }
        }, 10);

        await AddCards(mtgDumpCards.ToList(), userId);
    }

    public async Task AddCards(List<Card> cards, Guid userId)
    {
        await ImportCardTypes(cards);
        await ImportRarities(cards);

        await cards.ParallelForEachAsync((card, l) => AddCard(card, userId, l, true), 20);
    }

    public async Task AddCard(Card card, Guid userId, long index = 0, bool incQuantity = false)
    {
        var exists = await _mtgCardDao.CheckCardExistsAndIncQuantity(card.Name, card.Set, userId, incQuantity ? 1 : 0);
        try
        {
            if (!exists)
            {
                var type = card.TypeLine.Split('—')[0].Trim();
                var colors = new List<MtgCardColorEntity>();

                if (card.Colors != null)
                {
                    foreach (var color in card.Colors)
                    {
                        colors.Add(await _mtgCardColorDao.QueryAsSingle(entities =>
                            entities.Where(s => s.Name == color)));
                    }
                }

                var cardType =
                    await _mtgCardTypeDao.QueryAsSingle(entities => entities.Where(s => s.Name == type));
                var rarity =
                    await _mtgCardRarityDao.QueryAsSingle(entities => entities.Where(s => s.Name == card.Rarity));
                var setId = await _mtgCardSetDao.FindByCode(card.Set);

                var languageId = await _mtgCardLanguageDao.QueryAsSingle(entities =>
                    entities.Where(s => s.Code.ToLower() == card.Language.ToLower()));

                var imageHighRes = "";
                var imageLowRes = "";
                var foreignNames = "";

                if (card.MtgoId != 0)
                {
                    var mtgCard =
                        await _mtgDumpDao.QueryAsSingle(entities => entities.Where(s => s.MultiverseId == card.MtgoId));

                    if (mtgCard != null)
                    {
                        foreignNames = string.Join(',', mtgCard.Card.ForeignNames.Select(s => s.Name));
                    }

                    if (mtgCard == null)
                    {
                        mtgCard = await _mtgDumpDao.QueryAsSingle(entities =>
                            entities.Where(s => s.CardName.ToLower() == card.Name.ToLower()));

                        if (mtgCard != null)
                        {
                            foreignNames = string.Join(',', mtgCard.Card.ForeignNames.Select(s => s.Name));
                        }
                    }
                }

                if (card.ImageUris != null)
                {
                    if (card.ImageUris.ContainsKey("large"))
                    {
                        imageHighRes = card.ImageUris["large"].ToString();
                    }

                    if (card.ImageUris.ContainsKey("normal"))
                    {
                        imageLowRes = card.ImageUris["normal"].ToString();
                    }
                }

                var cardEntity = new MtgCardEntity()
                {
                    Name = card.Name ?? card.PrintedName,
                    PrintedName = card.PrintedName ?? card.Name,
                    TypeId = cardType.Id,
                    ManaCost = card.ManaCost ?? " ",
                    Cmc = card.Cmc,
                    Quantity = 1,
                    CardMarketId = card.CardMarketId,
                    HighResImageUrl = imageHighRes,
                    LowResImageUrl = imageLowRes,
                    TypeLine = card.TypeLine ?? " ",
                    Price = card.Prices.Eur ?? 0,
                    MtgId = card.MtgoId,
                    RarityId = rarity.Id,
                    OwnerId = userId,
                    LanguageId = languageId.Id,
                    ScryfallId = card.Id.ToString(),
                    Description = card.PrintedText ?? card.OracleText ?? "",
                    SetId = setId.Id,
                    ForeignNames = foreignNames,
                    IsColorLess = colors.Count == 0,
                    IsMultiColor = colors.Count > 1,
                };
                if (!string.IsNullOrEmpty(card.Power) && card.Power != "*")
                {
                    cardEntity.Power = int.Parse(card.Power);
                }

                if (!string.IsNullOrEmpty(card.Toughness) && card.Power != "*")
                {
                    cardEntity.Toughness = int.Parse(card.Toughness);
                }

                if (!string.IsNullOrEmpty(card.CollectorNumber) && Regex.IsMatch(card.CollectorNumber, @"^\d+$"))
                {
                    cardEntity.CollectorNumber = int.Parse(card.CollectorNumber);
                }

                await _mtgCardDao.Insert(cardEntity);

                foreach (var color in colors)
                {
                    await _mtgCardColorRelDao.Insert(new MtgCardColorRelEntity()
                    {
                        CardId = cardEntity.Id,
                        ColorId = color.Id,
                    });
                }

                if (card.Legalities != null)
                {
                    foreach (var legality in card.Legalities)
                    {
                        var legalityName = await _mtgCardLegalityDao.InsertIfNotExists(legality.Key);
                        var legalityType = await _mtgCardLegalityTypeDao.InsertIfNotExists(legality.Value);

                        await _mtgCardLegalityRelDao.Insert(new MtgCardLegalityRelEntity()
                        {
                            CardId = cardEntity.Id,
                            CardLegalityId = legalityName.Id,
                            CardLegalityTypeId = legalityType.Id
                        });
                    }
                }

                Logger.LogInformation("{Name} - {ManaCost} - total: {Mana} - [{Index}]", card.Name, card.ManaCost,
                    card.Cmc,
                    index);
                await PublishEvent(NotificationEventBuilder.Create().ToUser(userId)
                    .Type(NotificationEventType.Information).Title("Importing cards").Message($"Card {card.Name}")
                    .Build());
            }
        }
        catch (Exception ex)
        {
            Logger.LogError("Error during inserting card: {Card} - {Ex}", card.Name, ex.Message);
        }
    }

    private async Task ImportCardTypes(List<Card> cards)
    {
        var types = cards.DistinctBy(s => s.TypeLine).Select(s => s.TypeLine.Split('—')[0].Trim())
            .DistinctBy(s => s).Select(s => s).ToList();

        foreach (var type in types)
        {
            await _mtgCardTypeDao.AddIfNotExists(type);
        }
    }

    public async Task ImportRarities(List<Card> cards)
    {
        var types = cards.DistinctBy(s => s.Rarity).DistinctBy(s => s).Select(s => s.Rarity).ToList();

        foreach (var type in types)
        {
            await _mtgCardRarityDao.AddIfNotExists(type);
        }
    }

    private List<TFormat> ParseCsvFormFile<TFormat>(string fileName)
    {
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<TFormat>().ToList();
    }
}