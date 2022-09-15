using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards;


[Table("card_legalities")]
public class CardLegalityEntity : BaseGuidEntity
{
    public string Name { get; set; }
}