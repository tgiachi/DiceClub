using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards;


[Table("card_lagality_types")]
public class CardLegalityTypeEntity : BaseGuidEntity
{
    public string Name { get; set; }
    
}