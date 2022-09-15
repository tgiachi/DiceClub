using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using Aurora.Api.Entities.Interfaces.Dto;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Cards;
using DiceClub.Database.Entities.Cards.Deck;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;


[DataAccess]
public class CardLegalityDao : AbstractDataAccess<Guid, CardLegalityEntity, DiceClubDbContext>
{
    public CardLegalityDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardLegalityEntity> logger) : base(dbContext, logger)
    {
    }

    public Task<CardLegalityEntity> FindByName(string name)
    {
        return QueryAsSingle(entities => entities.Where(s => s.Name.ToLower() == name.ToLower()));
    }
    public async Task<CardLegalityEntity> CreateIfNotExists(string name)
    {
        var cardLegality = await QueryAsSingle(entities => entities.Where(s => s.Name.ToLower() == name.ToLower()));
        if (cardLegality == null)
        {
            cardLegality = new CardLegalityEntity
            {
              Name = name
            };

            return await Insert(cardLegality);
        }

        return cardLegality;
    }

}