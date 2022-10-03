using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

using DiceClub.Database.Entities.Account;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace DiceClub.Database.Entities.MtgCards
{

    [Table("mtg_cards")]
    [Index(new []{nameof(ScryfallId)})]
    [Index(new []{nameof(Name), nameof(PrintedName)})]
    [Index(new []{nameof(LanguageId)})]
    [Index(new []{nameof(TypeId), nameof(RarityId), nameof(SetId)})]
    [Index(new []{nameof(OwnerId)})]

    public class MtgCardEntity : BaseGuidEntity
    {

        [MaxLength(100)]
        public string ScryfallId { get; set; }

        public int? MtgId { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
        
        [MaxLength(600)]
        public string ForeignNames { get; set; }
        
        [MaxLength(150)]
        public string PrintedName { get; set; }

        public string TypeLine { get; set; }

        public string Description { get; set; }

        public Guid LanguageId { get; set; }

        public virtual MtgCardLanguageEntity Language { get; set; }

        [MaxLength(100)]
        public string? ManaCost { get; set; }

        public decimal? Cmc { get; set; }

        public int? Power { get; set; }

        public int? Toughness { get; set; }

        public int? CollectorNumber { get; set; }

        public Guid SetId { get; set; }

        public virtual MtgCardSetEntity Set { get; set; }

        public Guid RarityId { get; set; }
        public virtual MtgCardRarityEntity Rarity { get; set; }

        [MaxLength(150)]
        public string? LowResImageUrl { get; set; }

        [MaxLength(150)]
        public string? HighResImageUrl { get; set; }
        public Guid TypeId { get; set; }
        public virtual MtgCardTypeEntity Type { get; set; }
        
        public int? CardMarketId { get; set; }
        
        public int Quantity { get; set; }

        public virtual List<MtgCardColorRelEntity> Colors { get; set; }
        public virtual List<MtgCardLegalityRelEntity> Legalities { get; set; }

        public Guid OwnerId { get; set; }
        public DiceClubUser Owner { get; set; }

        public NpgsqlTsVector SearchVector { get; set; }

        public bool IsColorLess { get; set; }
        public bool IsMultiColor { get; set; }
        
        public decimal? Price { get; set; }
    }
}
