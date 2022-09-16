using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards;


[Table("card_symbols")]
public class CardSymbolEntity : BaseGuidEntity
{
    public string Symbol { get; set; }
    
    public string Image { get; set; }
}