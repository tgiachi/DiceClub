using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace DiceClub.Database.Entities.Deck
{

    [Table("deck_master")]
    [Index(new []{nameof(Name), nameof(OwnerId)})]
    public class DeckMasterEntity : BaseGuidEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string ColorIdentity { get; set; }
        
        public DeckFormat Format { get; set; }
        
        public virtual List<DeckDetailEntity> Details { get; set; }
        
        public int CardCount { get; set; }

        public Guid OwnerId { get; set; }

        public DiceClubUser Owner { get; set; }

    }

    public enum DeckFormat
    {
        Commander,
        FreeForm,
    }
}
