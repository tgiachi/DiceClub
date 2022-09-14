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

    public CardService(IEventBusService eventBusService, ILogger<CardService> logger, CardsDao cardsDao, ColorsDao colorsDao, RarityDao rarityDao) : base(
        eventBusService, logger)
    {
        _cardsDao = cardsDao;
        _colorsDao = colorsDao;
        _rarityDao = rarityDao;
    }

    public async Task<List<CardEntity>> SearchCards(CardQueryObject query)
    {

        var colorsGuids = new List<Guid>();
        var raririesGuids = new List<Guid>();

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