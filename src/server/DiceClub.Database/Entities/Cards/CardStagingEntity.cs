using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Account;

namespace DiceClub.Database.Entities.Cards;


[Table("cards_staging")]
public class CardStagingEntity : BaseGuidEntity
{
    public virtual DiceClubUser User { get; set; }
    public Guid UserId { get; set; }
    
    public int? MtgId { get; set; }
    
    public bool IsFoil { get; set; }
    
    public string Language { get; set; }
    
    public bool IsAdded { get; set; }
}