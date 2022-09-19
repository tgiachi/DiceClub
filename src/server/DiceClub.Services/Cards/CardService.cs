using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Cards.Mtg;
using DiceClub.Api.Events.Cards;
using DiceClub.Api.Utils;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.Cards;
using DiceClub.Services.Data.Card;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;
using ScryfallApi.Client;
using ScryfallApi.Client.Models;

namespace DiceClub.Services.Cards;

public class CardService : AbstractBaseService<CardService>, INotificationHandler<AddCardFromStagingEvent>
{
    private readonly CardsDao _cardsDao;
    private readonly ColorsDao _colorsDao;
    private readonly RarityDao _rarityDao;
    private readonly CardSetDao _cardSetDao;
    private readonly CardLegalityDao _cardLegalityDao;
    private readonly CardLegalityTypeDao _cardLegalityTypeDao;
    private readonly CardTypeDao _cardTypeDao;
    private readonly CreatureTypeDao _creatureTypeDao;
    private readonly CardStagingDao _cardStagingDao;
    private readonly ScryfallApiClient _scryfallApiClient;
    private readonly ColorCardDao _colorCardDao;
    private readonly CardCardLegalityDao _cardCardLegalityDao;

    private readonly MtgDao _mtgDao;


    public CardService(IEventBusService eventBusService, ILogger<CardService> logger, CardsDao cardsDao,
        ColorsDao colorsDao, RarityDao rarityDao, CardSetDao cardSetDao, CardLegalityDao cardLegalityDao,
        CardLegalityTypeDao cardLegalityTypeDao, CardTypeDao cardTypeDao, CreatureTypeDao creatureTypeDao,
        MtgDao mtgDao, CardStagingDao cardStagingDao, ScryfallApiClient scryfallApiClient, ColorCardDao colorCardDao,
        CardCardLegalityDao cardCardLegalityDao) : base(
        eventBusService, logger)
    {
        _cardsDao = cardsDao;
        _colorsDao = colorsDao;
        _rarityDao = rarityDao;
        _cardSetDao = cardSetDao;
        _cardLegalityDao = cardLegalityDao;
        _cardLegalityTypeDao = cardLegalityTypeDao;
        _cardTypeDao = cardTypeDao;
        _creatureTypeDao = creatureTypeDao;
        _mtgDao = mtgDao;
        _cardStagingDao = cardStagingDao;
        _scryfallApiClient = scryfallApiClient;
        _colorCardDao = colorCardDao;
        _cardCardLegalityDao = cardCardLegalityDao;
    }

    public Task<List<CardSetEntity>> FindAllSets()
    {
        return _cardSetDao.FindAll();
    }

    public Task<List<CardLegalityEntity>> FindAllLegalities()
    {
        return _cardLegalityDao.FindAll();
    }

    public Task<List<CardLegalityTypeEntity>> FindAllLegalityTypes()
    {
        return _cardLegalityTypeDao.FindAll();
    }

    public Task<List<CreatureTypeEntity>> FindAllCreatureTypes()
    {
        return _creatureTypeDao.FindAll();
    }

    public Task<List<CardTypeEntity>> FindAllCardTypes()
    {
        return _cardTypeDao.FindAll();
    }

    public Task<List<MtgEntity>> SearchAutoComplete(string name, string? setCode)
    {
        return _mtgDao.SearchCard(name, setCode);
    }

    public async Task<List<CardEntity>> SearchCards(CardQueryObject query)
    {
        var colorsGuids = new List<Guid>();
        var raririesGuids = new List<Guid>();
        var types = new List<Guid>();

        if (query.Colors != null)
        {
            foreach (var queryColor in query.Colors)
            {
                var color = await _colorsDao.FindByName(queryColor);
                if (color != null)
                {
                    colorsGuids.Add(color.Id);
                }
            }
        }

        if (query.Types != null)
        {
            foreach (var queryType in query.Types)
            {
                var type = await _cardTypeDao.FindByName(queryType);
                if (type != null)
                {
                    types.Add(type.Id);
                }
            }
        }

        if (query.Rarity != null)
        {
            foreach (var queryRarity in query.Rarity)
            {
                var rarity = await _rarityDao.FindByName(queryRarity);
                if (rarity != null)
                {
                    raririesGuids.Add(rarity.Id);
                }
            }
        }

        return await _cardsDao.QueryAsList(entities =>
        {
            entities = entities
                .Include(s => s.Rarity)
                .Include(j => j.ColorCards)
                .Include(k => k.CardType)
                .Include(o => o.CardSet);

            if (!string.IsNullOrEmpty(query.Name))
            {
                entities = entities.Where(s => s.CardName.ToLower().Contains(query.Name.ToLower()));
            }

            if (colorsGuids.Any())
            {
                entities = entities.Where(s => s.ColorCards.All(x => colorsGuids.Any(y => x.ColorId == y)));
            }

            if (raririesGuids.Any())
            {
                entities = entities.Where(s => raririesGuids.Contains(s.RarityId));
            }

            if (types.Any())
            {
                entities = entities.Where(s => types.Contains(s.CardTypeId));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                entities = entities.Where(s =>
                    EF.Functions.ToTsVector("italian", s.Description)
                        .Matches($"{query.Description}:*")
                );
            }

            return entities;
        });
    }

    public async Task AddCard(Card card, Guid userId, int index = 0)
    {
        try
        {
            var mana = TokenUtils.ExtractManaToken(card.ManaCost);
            var type = card.TypeLine.Split('—')[0].Trim();


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
                    ManaCost = card.ManaCost ?? " ",
                    TotalManaCosts = mana,
                    Quantity = 1,
                    ImageUrl = imageLink,
                    TypeLine = card.TypeLine ?? " ",
                    Price = card.Prices.Eur,
                    MtgId = card.MtgoId,
                    RarityId = rarity.Id,
                    UserId = userId,
                    Description = card.PrintedText ?? card.OracleText ?? "",
                    CardSetId = setId.Id,
                    IsColorLess = colors.Count == 0,
                    IlMultiColor = colors.Count > 1
                };

                if (!string.IsNullOrEmpty(card.CollectorNumber))
                {
                    try
                    {
                        var cn = int.Parse(card.CollectorNumber);
                        cardEntity.CollectionNumber = cn;
                    }
                    catch
                    {
                    }
                }

                if (!string.IsNullOrEmpty(card.TypeLine))
                {
                    if (card.TypeLine.ToLower().StartsWith("creature"))
                    {
                        var creatureTypeStr = card.TypeLine.Split('—')[1].Trim();
                        var creatureType = await _creatureTypeDao.FindByName(creatureTypeStr);

                        if (creatureType == null)
                        {
                            creatureType = await _creatureTypeDao.Insert(new CreatureTypeEntity
                            {
                                Name = creatureTypeStr
                            });
                        }
                        
                        cardEntity.CreatureTypeId = creatureType.Id;
                    }
                }

                await _cardsDao.Insert(cardEntity);

                foreach (var color in colors)
                {
                    await _colorCardDao.Insert(new ColorCardEntity()
                    {
                        CardId = cardEntity.Id,
                        ColorId = color.Id,
                    });
                }

                if (card.Legalities != null)
                {
                    foreach (var legality in card.Legalities)
                    {
                        var legalityName = await _cardLegalityDao.FindByName(legality.Key);
                        var legalityType = await _cardLegalityTypeDao.FindByName(legality.Value);

                        await _cardCardLegalityDao.Insert(new CardCardLegality()
                        {
                            CardId = cardEntity.Id,
                            CardLegalityId = legalityName.Id,
                            CardLegalityTypeId = legalityType.Id
                        });
                    }
                }

                Logger.LogInformation("{Name} - {ManaCost} - total: {Mana} - [{Index}]", card.Name, card.ManaCost, mana,
                    index);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError("Error during inserting card: {Ex}", ex);
        }
    }

    public async Task Handle(AddCardFromStagingEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Received staging card to add: {Id}", notification.StagingId);
        var stagingCard = await _cardStagingDao.FindById(notification.StagingId);
        var mtgEntity = await _mtgDao.FindByMultiverseId(stagingCard.MtgId.Value);

        MtgForeignName language = null;
        if (notification.Language != "en")
        {
            language =
                mtgEntity.Card.ForeignNames.FirstOrDefault(s =>
                    s.Language.ToLower().StartsWith(notification.Language.ToLower()));
        }

        if (language == null)
        {
            language = new MtgForeignName()
            {
                Name = mtgEntity.CardName
            };
        }


        var skCard = await _scryfallApiClient.Cards.Search(language.Name, 1, new SearchOptions
        {
            IncludeMultilingual = true
        });


        if (skCard.Data.ToList().Count == 1)
        {
            await AddCard(skCard.Data.FirstOrDefault(), stagingCard.UserId, 0);
        }
    }
}