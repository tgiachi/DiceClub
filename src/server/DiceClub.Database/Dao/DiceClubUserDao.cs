using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao
{

    [DataAccess]
    public class DiceClubUserDao : AbstractDataAccess<Guid, DiceClubUser, DiceClubDbContext>
    {
        public DiceClubUserDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<DiceClubUser> logger) : base(dbContext, logger)
        {
        }

        public async Task<DiceClubUser> InsertUser(DiceClubUser user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user = await Insert(user);
            return user;
        }
    }
}
