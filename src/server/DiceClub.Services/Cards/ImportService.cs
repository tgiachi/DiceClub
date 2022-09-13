using System.Collections.Concurrent;
using System.Globalization;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using CsvHelper;
using CsvHelper.Configuration;
using Dasync.Collections;
using DiceClub.Api.Data.Cards;
using DiceClub.Api.Data.Cards.Mtg;
using DiceClub.Api.Events.WebSocket;
using DiceClub.Api.Utils;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.Account;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;
using ScryfallApi.Client;
using ScryfallApi.Client.Models;

namespace DiceClub.Services.Cards
{
    public class ImportService : AbstractBaseService<ImportService>
    {
        private readonly ScryfallApiClient _scryfallApiClient;
        private readonly ColorCardDao _colorCardDao;
        private readonly ColorsDao _colorsDao;
        private readonly CardTypeDao _cardTypeDao;
        private readonly RarityDao _rarityDao;
        private readonly CardsDao _cardsDao;
        private readonly CardSetDao _cardSetDao;
        private readonly ImportMtgService _importMtgService;


        public ImportService(IEventBusService eventBusService,
            ILogger<ImportService> logger,
            ScryfallApiClient scryfallApiClient,
            ColorCardDao colorCardDao,
            ColorsDao colorsDao,
            CardTypeDao cardTypeDao,
            RarityDao rarityDao,
            CardsDao cardsDao,
            CardSetDao cardSetDao,
            ImportMtgService importMtgService
        ) : base(eventBusService, logger)
        {
            _scryfallApiClient = scryfallApiClient;
            _colorCardDao = colorCardDao;
            _colorsDao = colorsDao;
            _cardTypeDao = cardTypeDao;
            _rarityDao = rarityDao;
            _cardsDao = cardsDao;
            _cardSetDao = cardSetDao;
            _importMtgService = importMtgService;
        }

        public async Task ImportCardCastleCsv(string fileName, Guid clubUserId)
        {
            var records = await ImportCsv(fileName);
            var cards = await SearchCardsFromCsv(records);

            await ImportColors(cards);
            await ImportCardTypes(cards);
            await ImportRarities(cards);
            await ImportSets(cards);
            await AddCards(cards, clubUserId);
        }

        public async Task<List<CardCastleRecord>> ImportCsv(string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using var reader = new StreamReader(fileName);
            using var csv = new CsvReader(reader, config);

            await PublishEvent(new DiagnosticMessageEvent()
            {
                Message = $"Starting parsing file '{fileName}' ",
                Type = "info"
            });

            var records = csv.GetRecords<CardCastleRecord>().ToList();

            await PublishEvent(new DiagnosticMessageEvent()
            {
                Message = $"Parsed {records.Count} cards",
                Type = "info"
            });

            return records;
        }

        public async Task<List<Card>> SearchCardsFromCsv(List<CardCastleRecord> records)
        {
            var cards = new ConcurrentBag<Card>();


            await records.ParallelForEachAsync(async (record, i) =>
            {
                if (string.IsNullOrEmpty(record.Name)) return;


                try
                {
                    Logger.LogInformation("Searching card {Current}/{Count} - {Name}", i, records.Count, record.Name);

                    MtgCard foundCard = null;
                    if (record.MultiverseId != null)
                    {
                        foundCard = await _importMtgService.FindCardByMultiverseId((int)record.MultiverseId);
                    }
                    else
                    {
                        foundCard = await _importMtgService.FindCardByName(record.Name);
                    }

                    if (foundCard == null)
                    {
                        foundCard = await _importMtgService.FindByJsonId(record.JsonId);
                    }

                    if (foundCard == null)
                    {
                        var fallBackCards =
                            await _scryfallApiClient.Cards.Search(record.Name, 1, SearchOptions.CardSort.Name);
                        if (fallBackCards.Data.Count > 0)
                        {
                            var fallBackCard =
                                fallBackCards.Data.FirstOrDefault(card => card.Id == Guid.Parse(record.JsonId));
                            if (fallBackCard != null)
                            {
                                cards.Add(fallBackCard);
                                Logger.LogInformation("Added fallback card {Card} - {CardName}", fallBackCard.Name,
                                    record.Name);
                                return;
                            }
                         
                        }
                    }

                    if (foundCard == null)
                    {
                        Logger.LogWarning("Card {CardName} not found in any db:", record.Name);
                        return;
                    }

                    if (foundCard.ForeignNames != null &&
                        foundCard.ForeignNames.Count(s => s.Language == "Italian") > 0)
                    {
                        var italianCard = foundCard.ForeignNames.Where(s => s.Language == "Italian").Select(s => s)
                            .FirstOrDefault();
                        Logger.LogInformation("Found italian version of {CardName} [{ItalianName}]", foundCard.Name,
                            italianCard.Name);

                        var skCard = await _scryfallApiClient.Cards.Search(italianCard.Name, 1, new SearchOptions
                        {
                            IncludeMultilingual = true
                        });

                        if (skCard.Data.Count > 0)
                        {
                            cards.Add(skCard.Data.FirstOrDefault());
                        }
                    }
                    else
                    {
                        Logger.LogInformation("Italian card not found, fallback to english");
                        var englishCard = await _scryfallApiClient.Cards.Search(foundCard.Name, 1, new SearchOptions
                        {
                            Sort = SearchOptions.CardSort.Name,
                        });

                        if (englishCard.Data.Count > 0)
                        {
                            cards.Add(englishCard.Data.FirstOrDefault());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error during parse card {Card}: {Ex}", record.Name, ex);
                }
            }, 20);

            return cards.ToList();
        }

        public async Task ImportSets(List<Card> cards)
        {
            var sets = cards.DistinctBy(s => s.Set).Select(s => new { s.Set, s.SetName }).ToList();

            foreach (var set in sets)
            {
                await _cardSetDao.CreateIfNotExists(set.Set, set.SetName);
            }
        }

        public async Task ImportColors(List<Card> cards)
        {
            await PublishEvent(new DiagnosticMessageEvent()
            {
                Message = $"Adding colors",
                Type = "info"
            });
            var colors = cards.Where(s => s != null && s.Colors != null).SelectMany(s => s.Colors).DistinctBy(s => s)
                .ToList();

            foreach (var color in colors)
            {
                await _colorsDao.AddIfNotExists(color);
            }
        }

        private async Task ImportCardTypes(List<Card> cards)
        {
            var types = cards.DistinctBy(s => s.TypeLine).Select(s => s.TypeLine.Split('—')[0].Trim())
                .DistinctBy(s => s).Select(s => s).ToList();

            foreach (var type in types)
            {
                await _cardTypeDao.AddCardTypeIfNotExists(type);
            }
        }

        public async Task ImportRarities(List<Card> cards)
        {
            var types = cards.DistinctBy(s => s.Rarity).DistinctBy(s => s).Select(s => s.Rarity).ToList();

            foreach (var type in types)
            {
                await _rarityDao.AddCardTypeIfNotExists(type);
            }
        }

        private async Task AddCard(Card card, Guid userId)
        {
            try
            {
  var mana = TokenUtils.ExtractManaToken(card.ManaCost);
                var type = card.TypeLine.Split('—')[0].Trim();

                Logger.LogInformation("{Name} - {ManaCost} - total: {Mana}", card.Name, card.ManaCost, mana);
                var exists = await _cardsDao.CheckIfCardExists(card.Name);

                if (exists)
                {
                    await _cardsDao.IncrementQuantity(card.Name);
                }
                else
                {
                    var colors = new List<ColorEntity>();
                    
                    if (card.Colors != null)
                    {
                        foreach (var color in card.Colors)
                        {
                            colors.Add(await _colorsDao.QueryAsSingle(entities =>
                                entities.Where(s => s.Name == color)));
                        }
                    }
                    var cardType =
                        await _cardTypeDao.QueryAsSingle(entities => entities.Where(s => s.CardType == type));
                    var rarity = await _rarityDao.QueryAsSingle(entities => entities.Where(s => s.Name == card.Rarity));
                    var setId = await _cardSetDao.FindById(card.Set);

                    var imageLink = "";

                    if (card.ImageUris != null)
                    {
                        if (card.ImageUris.ContainsKey("large"))
                        {
                            imageLink = card.ImageUris["large"].ToString();
                        }
                        else
                        {
                            imageLink = card.ImageUris["normal"].ToString();
                        }
                    }
                    var cardEntity = new CardEntity()
                    {
                        CardName = card.PrintedName ?? card.Name,
                        // Colors = colors,
                        CardTypeId = cardType.Id,
                        ManaCost = card.ManaCost,
                        TotalManaCosts = mana,
                        Quantity = 1,
                        ImageUrl = imageLink,
                        Price = card.Prices.Eur,
                        MtgId = card.MtgoId,
                        RarityId = rarity.Id,
                        UserId = userId,
                        Description = card.PrintedText ?? card.OracleText ?? "",
                        CardSetId = setId.Id,
                    };
                    await _cardsDao.Insert(cardEntity);

                    foreach (var color in colors)
                    {
                        await _colorCardDao.Insert(new ColorCardEntity()
                        {
                            CardId = cardEntity.Id,
                            ColorId = color.Id,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error during inserting card: {Ex}", ex);
            }
        }

        public async Task AddCards(List<Card> cards, Guid userId)
        {
            foreach (var card in cards)
            {
                await AddCard(card, userId);
            }
        }
    }
}