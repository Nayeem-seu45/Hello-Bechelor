using Bechelor.Core.Domin.Bazar;
using Bechelor.Core.Domin.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses.BazarItems
{
    public interface IBazarItemService
    {
        Task<IEnumerable<BazarItem>> GetAllAsync();
        Task<BazarItem> GetByIdAsync(int id);
        Task<BazarItem> AddAsync(BazarItem model);
        Task<bool> UpdateAsync(BazarItem model);
        Task<bool> SoftDeleteByIdAsync(int id);
    }
}
 