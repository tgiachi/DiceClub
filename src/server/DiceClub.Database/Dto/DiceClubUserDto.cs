using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto
{
    public class DiceClubUserDto : AbstractDtoEntity<Guid>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Last { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string SerialId { get; set; }
        public string? RefreshToken { get; set; }
    }
}
