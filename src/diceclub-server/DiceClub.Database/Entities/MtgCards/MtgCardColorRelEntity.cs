using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.MtgCards
{

    [Table("mtg_colors_cards")]
    public class MtgCardColorRelEntity : BaseGuidEntity
    {
        public Guid CardId { get; set; }
        public virtual MtgCardEntity Card { get; set; }

        public Guid ColorId { get; set; }
        public virtual MtgCardColorEntity Color { get; set; }
    }
}
