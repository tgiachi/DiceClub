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
    public class UserGroupDao : AbstractDataAccess<Guid, UserGroup, DiceClubDbContext>
    {
        public UserGroupDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<UserGroup> logger) : base(dbContext, logger)
        {
        }
    }
}
