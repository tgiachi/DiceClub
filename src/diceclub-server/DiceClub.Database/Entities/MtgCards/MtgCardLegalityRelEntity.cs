using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.MtgCards
{

    [Table("mtg_cards_legalities")]
    public class MtgCardLegalityRelEntity : BaseGuidEntity
    {
        public Guid CardId { get; set; }
        public virtual MtgCardEntity Card { get; set; }

        public Guid CardLegalityId { get; set; }
        public virtual  MtgCardLegalityEntity CardLegality { get; set; }
    
        public virtual MtgCardLegalityTypeEntity CardLegalityType { get; set; }
        public Guid CardLegalityTypeId { get; set; }
    }
}
