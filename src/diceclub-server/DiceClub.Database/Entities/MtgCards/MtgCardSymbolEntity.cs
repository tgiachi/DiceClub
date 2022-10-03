using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.MtgCards;


[Table("mtg_symbols")]
public class MtgCardSymbolEntity : BaseGuidEntity
{
    
    [MaxLength(20)]
    public string Symbol { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    [MaxLength(150)]
    public string Image { get; set; }
}