using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Cards;
using DiceClub.Database.Dao.Cards.Deck;
using DiceClub.Database.Entities.Deck;
using DiceClub.Database.Entities.MtgCards;
using DiceClub.Services.Data.Cards.Deck;
using DiceClub.Web.Data.Rest.Cards.Deck;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class CardDeckService : AbstractBaseService<CardDeckService>
{
    private readonly CardService _cardService;
    private readonly DeckMasterDao _deckMasterDao;
    private readonly DeckDetailDao _deckDetailDao;

    public CardDeckService(IEventBusService eventBusService, ILogger<CardDeckService> logger, CardService cardService,
        DeckDetailDao deckDetailDao, DeckMasterDao deckMasterDao) :
        base(eventBusService, logger)
    {
        _cardService = cardService;
        _deckDetailDao = deckDetailDao;
        _deckMasterDao = deckMasterDao;
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

        foreach (var manaCurve in request.ManaCurves)
        {
            var filteredCards = new List<MtgCardEntity>();
            if (manaCurve.CmcCost <= 5)
            {
                filteredCards = cards.Where(s => s.Cmc == manaCurve.CmcCost).ToList();
            }
            else
            {
                filteredCards = cards.Where(s => s.Cmc >= manaCurve.CmcCost).ToList();
            }

            foreach (var i in Enumerable.Range(0, manaCurve.Count))
            {
                var randomCard = filteredCards[rnd.Next(filteredCards.Count)];
                var detail = await _deckDetailDao.Insert(new DeckDetailEntity
                {
                    Quantity = 1,
                    DeckMasterId = deckMaster.Id,
                    CardId = randomCard.Id,
                    CardType = DeckDetailCardType.Main
                });
            }
        }

        foreach (var i in Enumerable.Range(1, request.TotalSideBoard))
        {
            var sideBoardCard = cards[rnd.Next(cards.Count)];
            var sideDetail = await _deckDetailDao.Insert(new DeckDetailEntity
            {
                DeckMasterId = deckMaster.Id,
                CardId = sideBoardCard.Id,
                CardType = DeckDetailCardType.SideBoard
            });
        }
    }
}