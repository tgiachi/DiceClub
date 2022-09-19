using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Api.Data.Cards.Mtg;
using NpgsqlTypes;
using ScryfallApi.Client.Models;

namespace DiceClub.Database.Entities.Cards;


[Table("mtg_dump")]
public class MtgEntity : BaseGuidEntity
{
    public int? MultiverseId { get; set; }
    
    [Column(TypeName = "jsonb")]
    public MtgCard Card { get; set; }
    
    [MaxLength(300)]
    public string CardName { get; set; }
    
    [MaxLength(3000)]
    public string ForeignNames { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string SetCode { get; set; }
    
    public NpgsqlTsVector SearchVector { get; set; }
    
}