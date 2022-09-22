using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Account;

public class DiceClubUserDto : AbstractGuidDtoEntity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Last { get; set; }
    public string NickName { get; set; }
    public bool IsActive { get; set; }
    public string SerialId { get; set; }
}