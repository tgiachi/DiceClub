using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards;


[Table("card_sets")]
public class CardSetEntity : BaseGuidEntity
{
    [MaxLength(10)]
    public string SetCode { get; set; }
    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public virtual List<CardEntity> Cards { get; set; }

}