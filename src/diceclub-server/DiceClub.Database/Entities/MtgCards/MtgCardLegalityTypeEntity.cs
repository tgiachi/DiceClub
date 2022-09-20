using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.MtgCards
{
    public class MtgCardLegalityTypeEntity : BaseGuidEntity
    {

        [MaxLength(40)]
        public string Name { get; set; }
    }
}
