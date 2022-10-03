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
using RandomStringCreator;

namespace DiceClub.Database.Dao.Account
{

    [DataAccess]
    public class DiceClubUserDao : AbstractDataAccess<Guid, DiceClubUser, DiceClubDbContext>
    {
        public DiceClubUserDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<DiceClubUser> logger) : base(dbContext, logger)
        {
        }

        public async Task<DiceClubUser> InsertUser(DiceClubUser user)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt) + "#" + salt;
            user = await Insert(user);
            return user;
        }

        public async Task<DiceClubUser> Authenticate(string email, string password)
        {
            var user = await QueryAsSingle(users => users.Where(s => s.Email == email && s.IsActive).Include(s => s.UserGroups).ThenInclude(k => k.Group));
            if (user == null) return null;
            var salt = user.Password.Split("#")[1];
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            if (user.Password == hashedPassword + "#" + salt) return user;

            return null;
        }

        public async Task<bool> UpdateRefreshToken(string email, string refreshToken)
        {
            var user = await QueryAsSingle(users => users.Where(s => s.Email == email));
            if (user == null) return false;

            user.RefreshToken = refreshToken;
            await Update(user);

            return true;
        }

        public Task<DiceClubUser> FindUserByRefreshToken(string refreshToken)
        {
            return QueryAsSingle(users => users.Where(k => k.RefreshToken == refreshToken));
        }
        
        public async Task<List<string>> FindGroupsByUserId(Guid userId)
        {
            var groups = await QueryAsList<UserGroup>(users => users.Include(k => k.Group)
                .Where(s => s.UserId == userId));

            if (groups == null)
            {
                return new List<string>();
            }

            return groups.Select(s => s.Group.GroupName).ToList();



        }
    }
}
