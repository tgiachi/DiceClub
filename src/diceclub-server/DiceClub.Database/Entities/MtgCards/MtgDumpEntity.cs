using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Api.Data.Mtg;
using NpgsqlTypes;

namespace DiceClub.Database.Entities.MtgCards
{

    [Table("mtg_cards_dump")]
    public class MtgDumpEntity : BaseGuidEntity
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
}
