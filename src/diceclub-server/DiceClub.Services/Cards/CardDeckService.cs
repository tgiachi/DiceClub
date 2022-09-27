using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using Dasync.Collections;
using DiceClub.Api.Data.Cards;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dao.Cards.Deck;
using DiceClub.Database.Dto.Cards.Deck;
using DiceClub.Database.Entities.Deck;
using DiceClub.Database.Entities.MtgCards;
using DiceClub.Services.Data.Cards.Deck;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class CardDeckService : AbstractBaseService<CardDeckService>
{
    private readonly CardService _cardService;
    private readonly DeckMasterDao _deckMasterDao;
    private readonly DeckDetailDao _deckDetailDao;
    private readonly MtgCardTypeDao _mtgCardTypeDao;
    private readonly MtgCardColorDao _mtgCardColorDao;
    private readonly MtgCardColorRelDao _mtgCardColorRelDao;

    public CardDeckService(IEventBusService eventBusService, ILogger<CardDeckService> logger, CardService cardService,
        DeckDetailDao deckDetailDao, DeckMasterDao deckMasterDao, MtgCardTypeDao mtgCardTypeDao,
        MtgCardColorDao mtgCardColorDao, MtgCardColorRelDao mtgCardColorRelDao) :
        base(eventBusService, logger)
    {
        _cardService = cardService;
        _deckDetailDao = deckDetailDao;
        _deckMasterDao = deckMasterDao;
        _mtgCardTypeDao = mtgCardTypeDao;
        _mtgCardColorDao = mtgCardColorDao;
        _mtgCardColorRelDao = mtgCardColorRelDao;
    }

    public List<DeckManaCurvePreset> GetManaCurvePresets()
    {
        return new List<DeckManaCurvePreset>()
        {
            new DeckManaCurvePreset
            {
                Name = "Default",
                ManaCurve = new()
                {
                    new()
                    {
                        CmcCost = 1,
                        Count = 3
                    },
                    new()
                    {
                        CmcCost = 2,
                        Count = 7
                    },
                    new()
                    {
                        CmcCost = 3,
                        Count = 6
                    },
                    new()
                    {
                        CmcCost = 4,
                        Count = 3
                    },
                    new()
                    {
                        CmcCost = 5,
                        Count = 3
                    },
                    new()
                    {
                        CmcCost = 6,
                        Count = 3
                    }
                }
            }
        };
    }

    public async Task CreateMultipleRandomDeck(DeckMultipleDeckRequest request, Guid userId)
    {
        var random = new Random();
        await PublishEvent(new NotificationEventBuilder().ToUser(userId).Title("Deck").Message("Starting deck creation")
            .Build());

        await Enumerable.Range(0, request.Count).ParallelForEachAsync(async i =>
        {
            var rndColors = new List<string>();
            var numColors = random.Next(1, request.Colors.Count);
            foreach (var _ in Enumerable.Range(0, numColors))
            {
                rndColors.Add(request.Colors[random.Next(rndColors.Count)]);
            }
            await CreateRandomDeck(new DeckCreateRequest
            {
                DeckName = "Random deck #" + i,
                Colors = rndColors,
                TotalCards = request.TotalCards,
                TotalSideBoard = request.SideBoardTotalCards
            }, userId);
            
            await PublishEvent(new NotificationEventBuilder().ToUser(userId).Title("Deck").Message($"Deck #{i} done")
                .Build());

        }, 1);
        
        await PublishEvent(new NotificationEventBuilder().ToUser(userId).Title("Deck").Message("Deck creation done")
            .Build());
    }

    public async Task CreateRandomDeck(DeckCreateRequest request, Guid userId, int pageSize = 100)
    {
        var rnd = new Random();
        var pageCount = 0;
        var page = 1;
        var cards = new List<MtgCardEntity>();
        var maxLand = 30;
        var minLand = 23;
        var maxCopyOfCard = 4;
        var landQuantity = rnd.Next(minLand, maxLand);
        var cardsRemaining = request.TotalCards - landQuantity;
        var landCards = new Dictionary<string, string>()
        {
            { "W", "Plains" },
            { "U", "Island" },
            { "B", "Swamp" },
            { "R", "Mountain" },
            { "G", "Forest" }
        };

        var weightsBuilder = await BuildCardTypeWeights(cardsRemaining);

        if (request.ManaCurves == null)
        {
            request.ManaCurves = GetManaCurvePresets().FirstOrDefault(s => s.Name == "Default").ManaCurve;
        }

        var searchRequest = new SearchCardRequest()
        {
            Colors = request.Colors
        };

        var results = await _cardService.SearchCards(searchRequest, userId, page, pageSize);

        cards.AddRange(results.Cards);
        pageCount = (int)Math.Ceiling((double)results.TotalCount / pageSize);
        page++;
        while (page < pageCount)
        {
            results = await _cardService.SearchCards(searchRequest, userId, page, pageSize);
            cards.AddRange(results.Cards);
            page++;
        }

        var deckMaster = await _deckMasterDao.Insert(new DeckMasterEntity
        {
            Name = request.DeckName,
            OwnerId = userId
        });

        var deckDetails = new List<DeckDetailEntity>();

        foreach (var k in Enumerable.Range(0, cardsRemaining))
        {
            var w = weightsBuilder.PickAnItem();
            var filteredCards = cards.Where(s => s.TypeId == w.CardType.Id).ToList();

            var randomCard = filteredCards[rnd.Next(0, filteredCards.Count)];
            if (deckDetails.Count(s => s.CardId == randomCard.Id) > maxCopyOfCard)
            {
                continue;
            }

            Logger.LogInformation("Adding card: {CardName} - {Type}", randomCard.Name, randomCard.Type.Name);
            deckDetails.Add(new DeckDetailEntity
            {
                CardId = randomCard.Id,
                Quantity = 1,
                DeckMasterId = deckMaster.Id,
                CardType = DeckDetailCardType.Main
            });
        }

        await _deckDetailDao.InsertBulk(deckDetails);

        foreach (var k in Enumerable.Range(0, request.TotalSideBoard))
        {
            var randomType = weightsBuilder.PickAnItem();
            var filteredCards = cards.Where(s => s.TypeId == randomType.CardType.Id).ToList();
            var sideBoardCard = filteredCards[rnd.Next(filteredCards.Count)];

            Logger.LogInformation("SideBoard - Adding card: {CardName} - {Type}", sideBoardCard.Name,
                sideBoardCard.Type.Name);
            await _deckDetailDao.Insert(new DeckDetailEntity
            {
                CardId = sideBoardCard.Id,
                CardType = DeckDetailCardType.SideBoard,
                Quantity = 1,
                DeckMasterId = deckMaster.Id
            });
        }

        var numOfLandColors = (int)Math.Round((double)landQuantity / request.Colors.Count);
        foreach (var color in request.Colors)
        {

            var landOfColor = cards.FirstOrDefault(s => s.PrintedName.ToLower() == landCards[color].ToLower());

            await _deckDetailDao.Insert(new DeckDetailEntity
            {
                CardId = landOfColor.Id,
                DeckMasterId = deckMaster.Id,
                CardType = DeckDetailCardType.Land,
                Quantity = numOfLandColors
            });
        }
    }

    public Task<List<DeckMasterEntity>> FindDeckMasterByUserId(Guid userId)
    {
        return _deckMasterDao.QueryAsList(entities => entities.Where(s => s.OwnerId == userId));
    }

    public Task<List<DeckDetailEntity>> FindDeckDetailById(Guid id, Guid userId)
    {
        return _deckDetailDao.QueryAsList(entities => entities
            .Where(s => s.DeckMasterId == id && s.DeckMaster.OwnerId == userId)
            .Include(k => k.DeckMaster)
            .Include(k => k.Card)
        );
    }

    public async Task<RandomWeightedPicker<WeightedCardType>> BuildCardTypeWeights(int totalCards)
    {
        var weighted = new List<WeightedCardType>()
        {
            new()
            {
                CardType = await _mtgCardTypeDao.FindByName("Instant"),
                Weight = (int)Math.Round(0.2 * totalCards)
            },
            new()
            {
                CardType = await _mtgCardTypeDao.FindByName("Sorcery"),
                Weight = (int)Math.Round(0.2 * totalCards)
            },
            new()
            {
                CardType = await _mtgCardTypeDao.FindByName("Creature"),
                Weight = (int)Math.Round(0.3 * totalCards)
            },
            new()
            {
                CardType = await _mtgCardTypeDao.FindByName("Enchantment"),
                Weight = (int)Math.Round(0.1 * totalCards)
            },
            new()
            {
                CardType = await _mtgCardTypeDao.FindByName("Artifact"),
                Weight = (int)Math.Round(0.2 * totalCards)
            }
        };

        return new RandomWeightedPicker<WeightedCardType>(weighted);
    }
    
    
}