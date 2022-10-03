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
    [Table("mtg_sets")]
    public class MtgCardSetEntity : BaseGuidEntity
    {

        [MaxLength(10)]
        public string Code { get; set; }


        [MaxLength(150)]
        public string Description { get; set; }


        [MaxLength(200)]
        public string Image { get; set; }

        public int CardCount { get; set; }
    }
}
