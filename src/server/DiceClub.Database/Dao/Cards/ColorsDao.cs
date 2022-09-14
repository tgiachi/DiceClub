using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dao.Cards
{
    [DataAccess]

    public class ColorsDao : AbstractDataAccess<Guid, ColorEntity, DiceClubDbContext>
    {


        public async Task<ColorEntity> AddIfNotExists(string color)
        {
            var colorEntity = await QueryAsSingle(s => s.Where(k => k.Name == color));
            if (colorEntity == null)
            {
                colorEntity = new ColorEntity()
                {
                    Name = color
                };
                await Insert(colorEntity);
            }

            return colorEntity;
        }

        public Task<ColorEntity> FindByName(string name)
        {
            return QueryAsSingle(entities => entities.Where(s => s.Name.ToLower() == name.ToLower()));
        }
        public ColorsDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<ColorEntity> logger) : base(dbContext, logger)
        {
        }
    }
}
