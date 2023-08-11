using Bechelor.Core.Domin.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories;
using Hello_Bechelor.ServicesProvider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExpenseCategoryController : Controller
    {
        private readonly IExpenseCategoryService _expenseCategoryService;
        private readonly IRazorRenderService _renderService;

        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService, IRazorRenderService razorRenderService)
        {
            _expenseCategoryService = expenseCategoryService;
            _renderService = razorRenderService;
        }
        public IActionResult Index()
        {
            var model = new ExpenseCategory();
            return View(model);
        }

        public async Task<IActionResult> ViewAllLoad()
        {
            var data = await _expenseCategoryService.GetAllAsync();
            return PartialView("_ViewAll", data);
        }

        public async Task<IActionResult> OnGetAddOrEdit(int id = 0)
        {

            if (id == 0)
            {
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", new ExpenseCategory()) });
            }

            else
            {
                var expenseCategory = await _expenseCategoryService.GetByIdAsync(id);
                if (expenseCategory == null)
                {
                    return NotFound();
                }
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", expenseCategory) });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostAddOrEdit(int id, ExpenseCategory expenseCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        await _expenseCategoryService.AddAsync(expenseCategory);
                    }
                    else
                    {
                        await _expenseCategoryService.UpdateAsync(expenseCategory);
                    }
                    var expenseCategories = await _expenseCategoryService.GetAllAsync();
                    var html = await _renderService.ToStringAsync("_ViewAll", expenseCategories);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    var html = await _renderService.ToStringAsync("_CreateOrEdit", expenseCategory);
                    return new JsonResult(new { isValid = false, html = html });
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var expenseCategory = await _expenseCategoryService.SoftDeleteByIdAsync(id);

            if (expenseCategory == true)
            {
                var expensCategoryes = await _expenseCategoryService.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", expensCategoryes);
                return new JsonResult(new { isValid = true, html = html });
            }

            else
            {
                return null;
            }

        }
    }
}
