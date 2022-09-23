using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string ScryfallId { get; set; }
        
        public string CardName { get; set; }
        
        public string ImageUrl { get; set; }
        
        public virtual DiceClubUser User { get; set; }
        public Guid UserId { get; set; }
        public bool IsFoil { get; set; }

        public virtual MtgCardLanguageEntity Language { get; set; }
        public Guid LanguageId { get; set; }
        public bool IsAdded { get; set; }
        public int Quantity { get; set; }
    }
}
