using Bechelor.Core.Domin.Expenses;
using Bechelor.Infrastructure.Data;
using Bechelor.Services.Expenses.ExpenseCategories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses.ExpenseCategories
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IEntityRepository<ExpenseCategory> _expenseRepository;

        public ExpenseCategoryService(IEntityRepository<ExpenseCategory> entityRepository)
        {
            _expenseRepository = entityRepository;
        }
        public async Task<ExpenseCategory> AddAsync(ExpenseCategory model)
        {
            return await _expenseRepository.AddAsync(model);
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllAsync()
        {
           var data = await _expenseRepository.GetAllAsync();
           var dataList = data.Where(x => x.IsSoftDeleted == false).ToList();
            return dataList;
        }

        public async Task<ExpenseCategory> GetByIdAsync(int id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ExpenseCategoryItemModel>> GetCategoryItem()
        {
            List<ExpenseCategoryItemModel> categoryItems = new List<ExpenseCategoryItemModel>();
            IEnumerable<ExpenseCategory> categories = await _expenseRepository.GetAllAsync();
            foreach (var item in categories)
            {
                ExpenseCategoryItemModel category = new ExpenseCategoryItemModel()
                {
                    CategoryId = item.Id,
                    CategoryName = item.Name
                };
                categoryItems.Add(category);
            }
            return categoryItems;
        }

        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            return await _expenseRepository.SoftDeleteByIdAsync(id);
        }

        public Task<int> TotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ExpenseCategory model)
        {
           return await _expenseRepository.UpdateAsync(model);
        }
    }
}
