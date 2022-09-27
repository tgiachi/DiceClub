using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Cards;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dao.Cards.Deck;
using DiceClub.Database.Entities.Deck;
using DiceClub.Database.Entities.MtgCards;
using DiceClub.Services.Data.Cards.Deck;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class CardDeckService : AbstractBaseService<CardDeckService>
{
    private readonly CardService _cardService;
    private readonly DeckMasterDao _deckMasterDao;
    private readonly DeckDetailDao _deckDetailDao;
    private readonly MtgCardTypeDao _mtgCardTypeDao;

    public CardDeckService(IEventBusService eventBusService, ILogger<CardDeckService> logger, CardService cardService,
        DeckDetailDao deckDetailDao, DeckMasterDao deckMasterDao, MtgCardTypeDao mtgCardTypeDao) :
        base(eventBusService, logger)
    {
        _cardService = cardService;
        _deckDetailDao = deckDetailDao;
        _deckMasterDao = deckMasterDao;
        _mtgCardTypeDao = mtgCardTypeDao;
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
        var weights = await BuildWeights();
        
        if (request.ManaCurves == null)
        {
            request.ManaCurves = GetManaCurvePresets().FirstOrDefault(s => s.Name == "Default").ManaCurve;
        }

        Mutate(5, weights);

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

        var distCardRemains = cardsRemaining;
        foreach (var t in weights)
        {
            t.CardCount = (int)Math.Round(t.Weight * distCardRemains);
            distCardRemains = cardsRemaining - t.CardCount;
        }
        var totalCards = weights.Sum(s => s.CardCount);
        Logger.LogInformation("Total cards: {Total} adjusting size: {Result} ", totalCards, totalCards > cardsRemaining);

        if (totalCards > cardsRemaining)
        {
            while (weights.Sum(s => s.CardCount) > cardsRemaining)
            {
                var removeNumCards = rnd.Next(1, 2);
                var randomType = rnd.Next(weights.Count);
                if (weights[randomType].CardCount > 0)
                {
                    weights[randomType].CardCount -= removeNumCards;
                }
            }
        }

        var deckDetails = new List<DeckDetailEntity>();
        
        foreach (var w in weights)
        {
            var filteredCards = cards.Where(s => s.TypeId == w.Type.Id).ToList();
            while (w.CardCount != 0)
            {
                var randomCard = filteredCards[rnd.Next(0, filteredCards.Count)];
                if (deckDetails.Count(s => s.CardId == randomCard.Id) > maxCopyOfCard)
                {
                    continue;
                }
                deckDetails.Add(new DeckDetailEntity
                {
                    CardId = randomCard.Id,
                    Quantity = 1,
                    DeckMasterId = deckMaster.Id
                });
                w.CardCount--;
            }
        }

        await _deckDetailDao.InsertBulk(deckDetails);
    }

    public static void Mutate(int passes, List<DeckRandomCardData> weights)
    {
        var fudge = 0.33; // this adjusts our mutation amplitude
        var totalWeights = weights.Select(s => s.Weight).ToList();
        var rand = new Random();
        for (int i = 0; i < passes; i++)
        {
            var rand_a = 0;
            var rand_b = 0;
            while (rand_a == rand_b)
            {
                rand_a = rand.Next(0, weights.Count - 1);
                rand_b = rand.Next(0, weights.Count - 1);
            }

            var mutationMagnitude = (float)(rand.NextDouble() * totalWeights.Average() * fudge);
            //Console.WriteLine("mutation magnitude = " + mutationMagnitude);
            if (totalWeights[rand_a] - mutationMagnitude <= 0.1 || totalWeights[rand_b] + mutationMagnitude >= 0.90)
            {
                continue;
            }

            totalWeights[rand_a] -= mutationMagnitude;
            totalWeights[rand_b] += mutationMagnitude;
        }

        var idx = 0;
        foreach (var w in weights)
        {
            weights[idx].Weight = totalWeights[idx];
        }
    }

    private async Task<List<DeckRandomCardData>> BuildWeights()
    {
        var rand = new Random();
        var dict = new List<DeckRandomCardData>()
        {
            new ()
            {
                Type = await _mtgCardTypeDao.FindByName("Instant"),
                Weight = 1.0 / rand.Next(2, 5),
            },
            new ()
            {
                Type = await _mtgCardTypeDao.FindByName("Sorcery"),
                Weight = 1.0 / rand.Next(2, 5),
            },
            new ()
            {
                Type = await _mtgCardTypeDao.FindByName("Creature"),
                Weight = 1.0 / 1
            },
            new ()
            {
                Type = await _mtgCardTypeDao.FindByName("Enchantment"),
                Weight = 1.0 / rand.Next(2, 5)
            },
            new ()
            {
                Type = await _mtgCardTypeDao.FindByName("Artifact"),
                Weight = 1.0 / rand.Next(2, 5)
            }
        };

        return dict;
    }
}