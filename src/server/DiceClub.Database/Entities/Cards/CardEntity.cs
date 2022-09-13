using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Account;
using Mtg.Collection.Manager.Database.Entities;
using NpgsqlTypes;

namespace DiceClub.Database.Entities.Cards
{

    [Table("cards")]
    public class CardEntity : BaseGuidEntity
    {
        public string CardName { get; set; }

        public string ManaCost { get; set; }

        public int TotalManaCosts { get; set; }

        public int? MtgId { get; set; }

        public decimal? Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public virtual List<ColorCardEntity> ColorCards { get; set; }
        public Guid CardTypeId { get; set; }
        public virtual CardTypeEntity CardType { get; set; }

        public Guid RarityId { get; set; }

        public virtual RarityEntity Rarity { get; set; }

        public Guid? CreatureTypeId { get; set; }
        public virtual CreatureTypeEntity CreatureType { get; set; }
        
        public Guid CardSetId { get; set; }
        
        public virtual CardSetEntity CardSet { get; set; }

        public Guid UserId { get; set; }

        public DiceClubUser User { get; set; }
        
        public NpgsqlTsVector? SearchVector { get; set; }
        
        [MaxLength(3000)]
        public string Description { get; set; }
    }
}
