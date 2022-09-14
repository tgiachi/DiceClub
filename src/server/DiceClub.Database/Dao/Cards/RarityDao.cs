using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dao.Cards
{
    [DataAccess]

    public class RarityDao : AbstractDataAccess<Guid, RarityEntity, DiceClubDbContext>
    {
        public RarityDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<RarityEntity> logger) : base(dbContext, logger)
        {
        }

        public Task<RarityEntity> FindByName(string name)
        {
            return QueryAsSingle(entities => entities.Where(s => s.Name.ToLower() == name.ToLower()));
        }

        public async Task<RarityEntity> AddCardTypeIfNotExists(string type)
        {
            var exists = await QueryAsSingle(entities => entities.Where(s => s.Name == type));
            if (exists == null)
            {
                exists = await Insert(new RarityEntity
                {
                    Name = type
                });
            }

            return exists;
        }
    }
}
