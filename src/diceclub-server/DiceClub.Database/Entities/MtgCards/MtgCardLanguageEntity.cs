using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.MtgCards
{

    [Table("mtg_languages")]
    public class MtgCardLanguageEntity : BaseGuidEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public virtual List<MtgCardEntity> Cards { get; set; }
    }
}
