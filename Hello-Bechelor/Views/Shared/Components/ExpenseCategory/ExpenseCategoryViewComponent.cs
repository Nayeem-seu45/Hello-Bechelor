using Bechelor.Core.Domin.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Views.Shared.Components.ExpenseCategories
{
    public class ExpenseCategoryViewComponent : ViewComponent
    {
        private readonly IExpenseCategoryService _expenseCategory;
        public ExpenseCategoryViewComponent(IExpenseCategoryService expenseCategory)
        {
            _expenseCategory = expenseCategory;
        }
        public async Task<IViewComponentResult> InvokeAsync( string selectedId, int expenseCategoryId, string lableClass, bool isRequired = true)
        {
            ViewBag.SelectedId = selectedId;
            ViewBag.ExpenseCategoryId = expenseCategoryId;
            ViewBag.LableClass = lableClass;
            ViewBag.IsRequired = isRequired;
            ViewBag.ExpenseCategoryList = await GetAllExpenseCategory();
            return View();
        }

        private async Task<List<ExpenseCategory>> GetAllExpenseCategory()
        {
            List<ExpenseCategory> expenseCategories = (await _expenseCategory.GetAllAsync()).ToList();
            return  expenseCategories;
        }
    }
}
