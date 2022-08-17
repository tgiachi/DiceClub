using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiceClub.Database.Entities.Account
{
    [Table("users")]
    [Index(nameof(Email), IsUnique = true)]
    public class DiceClubUser : BaseGuidEntity
    {
        public string Email { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Last { get; set; }

        public string NickName { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public List<UserGroup> UserGroups { get; set; }

        [MaxLength(250)]
        public string SerialId { get; set; }
    }
}
