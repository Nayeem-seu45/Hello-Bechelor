using Bechelor.Core.Domin.Expenses;
using Bechelor.Services.Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseViewModel>> GetAllAsync();
        Task<ExpenseViewModel> GetByIdAsync(int id);
        Task<Expense> AddAsync(ExpenseViewModel model);
        Task<bool> UpdateAsync(ExpenseViewModel model);
        Task<int> TotalCountAsync();
        Task<bool> SoftDeleteByIdAsync(int id);
    }
}
