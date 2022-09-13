using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.Cards;
using DiceClub.Services.Data.Card;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class CardService : AbstractBaseService<CardService>
{
    private readonly CardsDao _cardsDao;

    public CardService(IEventBusService eventBusService, ILogger<CardService> logger, CardsDao cardsDao ) : base(
        eventBusService, logger)
    {
        _cardsDao = cardsDao;
    }

    public  Task<List<CardEntity>> SearchCards(CardQueryObject query)
    {
        return _cardsDao.QueryAsList(entities =>
        {
            if (!string.IsNullOrEmpty(query.Name))
            {
                entities = entities.Where(s => s.CardName.ToLower().Contains(query.Name.ToLower()));
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