using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.MtgCards
{
    [Table("mtg_legalities")]
    public class MtgCardLegalityEntity : BaseGuidEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
