using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;


[DataAccess]
public class CardLegalityTypeDao : AbstractDataAccess<Guid, CardLegalityTypeEntity, DiceClubDbContext>
{
    public CardLegalityTypeDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardLegalityTypeEntity> logger) : base(dbContext, logger)
    {
    }
    
    public Task<CardLegalityTypeEntity> FindByName(string name)
    {
        return QueryAsSingle(entities => entities.Where(s => s.Name.ToLower() == name.ToLower()));
    }
    public async Task<CardLegalityTypeEntity> CreateIfNotExists(string name)
    {
        var cardLegality = await QueryAsSingle(entities => entities.Where(s => s.Name.ToLower() == name.ToLower()));
        if (cardLegality == null)
        {
            cardLegality = new CardLegalityTypeEntity
            {
                Name = name
            };

            return await Insert(cardLegality);
        }

        return cardLegality;
    }
}