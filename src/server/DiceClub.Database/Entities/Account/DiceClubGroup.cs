using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Account
{
    [Table("groups")]
    public class DiceClubGroup : BaseGuidEntity
    {
        public string GroupName { get; set; }

        public bool IsAdmin { get; set; }

        public List<UserGroup> UserGroups { get; set; }
    }
}
