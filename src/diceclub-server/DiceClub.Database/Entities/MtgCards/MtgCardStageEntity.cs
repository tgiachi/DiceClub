using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Account;

namespace DiceClub.Database.Entities.MtgCards
{

    [Table("cards_staging")]
    public class MtgCardStageEntity : BaseGuidEntity
    {
        public virtual DiceClubUser User { get; set; }
        public Guid UserId { get; set; }

        public int? MtgId { get; set; }

        public bool IsFoil { get; set; }

        public virtual MtgCardLanguageEntity Language { get; set; }
        public Guid LanguageId { get; set; }

        public bool IsAdded { get; set; }
    }
}
