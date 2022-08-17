using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Account
{

    [Table("user_groups")]
    public class UserGroup : BaseGuidEntity
    {
        public Guid UserId { get; set; }
        public DiceClubUser User { get; set; }

        public Guid GroupId { get; set; }

        public DiceClubGroup Group { get; set; }
    }
}
