using DiceClub.Api.Attributes.Rest;
using DiceClub.Database.Context;
using DiceClub.Database.Dao.Account;
using DiceClub.Database.Dto.Account;
using DiceClub.Database.Dto.Mappers.Account;
using DiceClub.Database.Entities.Account;
using DiceClub.Services.Base;
using DiceClub.Services.Paginator;
using Microsoft.AspNetCore.Authorization;

namespace DiceClub.Web.Controllers.Account;


[Authorize]
[VerifyAuthGroup("ADMINS")]
public class UsersController : BaseCrudAuthController<DiceClubUser, DiceClubUserDto, DiceClubDbContext>
{
    public UsersController(DiceClubUserDao diceClubUserDao, ILogger<UsersController> logger, DiceClubUserDao dataAccess,
        DiceClubUserMapper dtoMapper, DiceClubDbContext dbContext, RestPaginatorService restPaginatorService) : base(
        diceClubUserDao, logger, dataAccess, dtoMapper, dbContext, restPaginatorService)
    {
    }
}