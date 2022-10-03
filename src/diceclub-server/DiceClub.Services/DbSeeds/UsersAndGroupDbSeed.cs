using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Attributes;
using Aurora.Api.Entities.Impl.Seeds;
using Aurora.Api.Entities.Interfaces.Dao;
using DiceClub.Database.Dao.Account;
using DiceClub.Database.Entities.Account;
using Microsoft.Extensions.Logging;
using RandomStringCreator;

namespace DiceClub.Services.DbSeeds
{

    [DbSeed(0)]
    public class UsersAndGroupDbSeed : AbstractDbSeed<Guid, DiceClubUser>
    {
        private readonly DiceClubGroupDao _diceClubGroupDao;
        private readonly UserGroupDao _userGroupDao;

        public UsersAndGroupDbSeed(DiceClubUserDao dao, DiceClubGroupDao diceClubGroupDao, UserGroupDao userGroupDao,
            ILogger<AbstractDbSeed<Guid, DiceClubUser>> logger) : base(dao, logger)
        {
            _diceClubGroupDao = diceClubGroupDao;
            _userGroupDao = userGroupDao;
        }

        public override async Task<bool> Seed()
        {
            _logger.LogInformation("Checking default groups");
            var usersGroupName = await _diceClubGroupDao.QueryAsSingle(groups => groups.Where(s => s.GroupName == "USERS"));
            var adminGroupName = await _diceClubGroupDao.QueryAsSingle(groups => groups.Where(s => s.GroupName == "ADMINS"));

            usersGroupName ??= await _diceClubGroupDao.Insert(new DiceClubGroup { GroupName = "USERS" });
            adminGroupName ??= await _diceClubGroupDao.Insert(new DiceClubGroup { GroupName = "ADMINS", IsAdmin = true });

            var adminUser = await Dao.QueryAsSingle(users => users.Where(s => s.Email == "squid@stormwind.it"));

            if (adminUser == null)
            {
                var randomPassword = new StringCreator().Get(10);
                adminUser ??= await ((DiceClubUserDao)Dao).InsertUser(new DiceClubUser
                {
                    Email = "squid@stormwind.it",
                    Name = "Admin",
                    Last = "User",
                    IsActive = true,
                    Password = randomPassword,
                    NickName = "Admin",
                    SerialId = "0"

                });

                await _userGroupDao.Insert(new UserGroup { GroupId = adminGroupName.Id, UserId = adminUser.Id });

                _logger.LogInformation("User added: {User} Password: {Password}", adminUser.Email, randomPassword);

            }

            return true;
        }
    }
}