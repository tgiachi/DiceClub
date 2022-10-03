using System.Reflection;
using DiceClub.Api.Attributes.Rest;
using DiceClub.Database.Dao.Account;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Base;

public class BaseGroupAuthController : BaseAuthController
{
    private readonly DiceClubUserDao _diceClubUserDao;
    protected ILogger Logger { get; set; }

    public BaseGroupAuthController(DiceClubUserDao diceClubUserDao, ILogger<BaseGroupAuthController> logger)
    {
        _diceClubUserDao = diceClubUserDao;
        Logger = logger;
    }

    protected async Task<bool> UserCanUseRoute()
    {
        var attribute = GetType().GetCustomAttribute<VerifyAuthGroupAttribute>();

        if (attribute == null)
        {
            return true;
        }

        var groups = await _diceClubUserDao.FindGroupsByUserId(GetUserId());

        return attribute.GroupNames.Split(',').Any(s => groups.Contains(s));
    }
    
}