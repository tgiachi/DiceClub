
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Attributes;
using Aurora.Api.Entities.Impl.Seeds;
using Aurora.Api.Entities.Interfaces.Dao;
using Aurora.Api.Entities.Interfaces.Seeds;
using Aurora.Api.Interfaces.Services;
using DiceClub.Api.Events.Accounts;
using DiceClub.Database.Dao;
using DiceClub.Database.Entities.Account;
using Microsoft.Extensions.Logging;
using RandomStringCreator;

namespace DiceClub.Database.Seeds
{
    [DbSeed(0)]
    public class UserAndGroupsSeed : AbstractDbSeed<Guid, DiceClubUser>
    {

        private readonly DiceClubUserDao _userDao;
        private readonly DiceClubGroupDao _groupDao;
        private readonly IEventBusService _eventBusService;
        private readonly UserGroupDao _userGroupDao;

        public UserAndGroupsSeed(DiceClubUserDao userDao, DiceClubGroupDao groupDao, UserGroupDao userGroupDao, ILogger<AbstractDbSeed<Guid, DiceClubUser>> logger, IEventBusService eventBusService) : base(userDao, logger)
        {
            _userDao = userDao;
            _groupDao = groupDao;
            _eventBusService = eventBusService;
            _userGroupDao = userGroupDao;
        }

        public override async Task<bool> Seed()
        {
            _logger.LogInformation("Checking default groups");
            var usersGroupName = await _groupDao.QueryAsSingle(groups => groups.Where(s => s.GroupName == "USERS"));
            var adminGroupName = await _groupDao.QueryAsSingle(groups => groups.Where(s => s.GroupName == "ADMINS"));

            usersGroupName ??= await _groupDao.Insert(new DiceClubGroup { GroupName = "USERS" });
            adminGroupName ??= await _groupDao.Insert(new DiceClubGroup { GroupName = "ADMINS", IsAdmin = true });

            var adminUser = await _userDao.QueryAsSingle(users => users.Where(s => s.Email == "squid@stormwind.it"));

            if (adminUser == null)
            {
                var randomPassword = new StringCreator().Get(10);
                adminUser ??= await _userDao.InsertUser(new DiceClubUser
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

                await _eventBusService.PublishEvent(new AccountCreatedEvent()
                {
                    UserId = adminUser.Id
                });
            }




            return true;
        }
    }
}
