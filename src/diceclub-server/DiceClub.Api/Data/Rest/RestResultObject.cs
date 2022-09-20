using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceClub.Api.Data.Rest
{
    public class RestResultObject<TData>
    {
        public TData Result { get; set; }

        public string? Error { get; set; }

        public bool HaveError { get; set; }
    }
}
