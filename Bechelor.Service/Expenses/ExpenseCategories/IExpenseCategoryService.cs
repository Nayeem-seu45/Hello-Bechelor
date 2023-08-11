using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bechelor.Core.Domin.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories.Models;

namespace Bechelor.Services.Expenses.ExpenseCategories
{
    public interface IExpenseCategoryService
    {
        Task<IEnumerable<ExpenseCategory>> GetAllAsync();
        Task<ExpenseCategory> GetByIdAsync(int id);
        Task<IEnumerable<ExpenseCategoryItemModel>> GetCategoryItem();
        Task<ExpenseCategory> AddAsync(ExpenseCategory model);
        Task<bool> UpdateAsync(ExpenseCategory model);
        Task<int> TotalCountAsync();
        Task<bool> SoftDeleteByIdAsync(int id);
    }
}
