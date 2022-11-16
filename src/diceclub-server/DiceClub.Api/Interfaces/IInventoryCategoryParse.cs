using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceClub.Api.Data.Inventory;

namespace DiceClub.Api.Interfaces
{
    public interface IInventoryCategoryParse
    {
        Task<InventoryCategorySearchResult> Search(string ean);
    }
}
