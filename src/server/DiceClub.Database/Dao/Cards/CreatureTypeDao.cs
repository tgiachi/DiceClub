using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dao.Cards
{
    [DataAccess]


    public class CreatureTypeDao : AbstractDataAccess<Guid, CreatureTypeEntity, DiceClubDbContext>
    {

        public CreatureTypeDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CreatureTypeEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<CreatureTypeEntity> AddIfNotExists(string type)
        {
            var creatureType = await QueryAsSingle(s => s.Where(k => k.Name == type));
            if (creatureType == null)
            {
                creatureType = new CreatureTypeEntity()
                {
                    Name = type
                };
                await Insert(creatureType);
            }

            return creatureType;
        }
    }
}
