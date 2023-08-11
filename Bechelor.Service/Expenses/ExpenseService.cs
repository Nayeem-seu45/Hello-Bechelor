using Bechelor.Core.Common;
using Bechelor.Core.Domin.Expenses;
using Bechelor.Infrastructure.Data;
using Bechelor.Services.Expenses.ExpenseCategories;
using Bechelor.Services.Expenses.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses
{
    public class ExpenseService : IExpenseService
    {
        private readonly IEntityRepository<Expense> _expenseRepository;
        private readonly IEntityRepository<ExpenseCategory> _expenseCategory;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExpenseService(IEntityRepository<Expense> expenseRepository, UserManager<ApplicationUser> userManager, IEntityRepository<ExpenseCategory> expenseCategory)
        {
            _userManager = userManager;
            _expenseRepository = expenseRepository;
            _expenseCategory = expenseCategory;
        }
        public async Task<Expense> AddAsync(ExpenseViewModel viewModel)
        {
            Expense expense = AddMappingProperties(ref viewModel);
            return await _expenseRepository.AddAsync(expense);
        }

        public async Task<IEnumerable<ExpenseViewModel>> GetAllAsync()
        {
           var expenseList = (await _expenseRepository.GetAllAsync()).Where(x=>x.IsSoftDeleted==false).ToList();
            List<ExpenseViewModel> expenseViews = new List<ExpenseViewModel>();
            foreach (var item in expenseList)
            {
                ExpenseViewModel model = new ()
                {
                    Id= item.Id, 
                    TotalAmount = item.TotalAmount,
                    Comment = item.Comment,
                    ExpenseDate =  item.ExpenseDate,
                    ExpenseCategoryId = item.ExpenseCategoryId,
                    ExpenseBy = item.ExpenseBy,
                    ExpenseCategoryName = (await _expenseCategory.GetByIdAsync(item.ExpenseCategoryId)).Name
                 
                };
                expenseViews.Add(model);
                
            }
            return expenseViews;
        }

        public async Task<ExpenseViewModel> GetByIdAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            return GetMappingProperties(ref expense);
        }

        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            return await _expenseRepository.SoftDeleteByIdAsync(id);
        }

        public Task<int> TotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ExpenseViewModel viewModel)
        {
            Expense expense = UpdateMappingProperties(ref viewModel);

            return await _expenseRepository.UpdateAsync(expense);
        }

        private Expense AddMappingProperties(ref ExpenseViewModel viewModel)
        {
            Expense expense = new ()
            {
                ExpenseDate = viewModel.ExpenseDate,
                ExpenseBy = viewModel.ExpenseBy,
                Comment = viewModel.Comment,
                TotalAmount = viewModel.TotalAmount,
                ExpenseCategoryId = viewModel.ExpenseCategoryId,
                CreatedOn = DateTime.Now
            };
            return expense;
        }
        private ExpenseViewModel GetMappingProperties(ref Expense expense)
        {
            ExpenseViewModel model = new ExpenseViewModel()
            {
                Id = expense.Id,
                ExpenseDate = expense.ExpenseDate,
                ExpenseBy = expense.ExpenseBy,
                Comment = expense.Comment,
                TotalAmount = expense.TotalAmount,
                ExpenseCategoryId =expense.ExpenseCategoryId,
                ExpenseCategory = _expenseCategory.GetByIdAsync(expense.ExpenseCategoryId).Result
            };
          
            return model;

        }

        private Expense UpdateMappingProperties(ref ExpenseViewModel viewModel)
        {
            Expense expense = new Expense()
            {
                Id = viewModel.Id,
                ExpenseDate = viewModel.ExpenseDate,
                ExpenseBy = viewModel.ExpenseBy,
                Comment = viewModel.Comment,
                TotalAmount = viewModel.TotalAmount,
                ExpenseCategoryId = viewModel.ExpenseCategoryId,
            };
            return expense;
        }


    }
}
