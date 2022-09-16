using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.Cards;
using DiceClub.Services.Data.Card;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class CardService : AbstractBaseService<CardService>
{
    private readonly CardsDao _cardsDao;
    private readonly ColorsDao _colorsDao;
    private readonly RarityDao _rarityDao;
    private readonly CardSetDao _cardSetDao;
    private readonly CardLegalityDao _cardLegalityDao;
    private readonly CardLegalityTypeDao _cardLegalityTypeDao;
    private readonly CardTypeDao _cardTypeDao;
    private readonly CreatureTypeDao _creatureTypeDao;


    public CardService(IEventBusService eventBusService, ILogger<CardService> logger, CardsDao cardsDao,
        ColorsDao colorsDao, RarityDao rarityDao, CardSetDao cardSetDao, CardLegalityDao cardLegalityDao,
        CardLegalityTypeDao cardLegalityTypeDao, CardTypeDao cardTypeDao, CreatureTypeDao creatureTypeDao) : base(
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
}