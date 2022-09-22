using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Account;

public class DiceClubGroupDto : AbstractGuidDtoEntity
{
    public string GroupName { get; set; }

    public bool IsAdmin { get; set; }
}