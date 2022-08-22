using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceClub.Api.Interfaces.Parsers
{
    public interface ICategoryParser<TOutput>
    {
        Task<TOutput> Parse(string serial);
    }
}
