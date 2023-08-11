using Bechelor.Core.Domin.Expenses;
using Bechelor.Services.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories;
using Bechelor.Services.Expenses.ExpenseCategories.Models;
using Bechelor.Services.Expenses.Models;
using Hello_Bechelor.ServicesProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IRazorRenderService _renderService;
        private readonly IExpenseCategoryService _expenseCategory;

        public ExpenseController(IExpenseCategoryService expenseCategory, IExpenseService expenseService, IRazorRenderService razorRenderService)
        {
            _expenseService = expenseService;
            _renderService = razorRenderService;
            _expenseCategory = expenseCategory;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["ExpenseCategories"] = new SelectList(await _expenseCategory.GetAllAsync(), "Id", "Name");
            var model = new ExpenseViewModel();
            return View(model);
        }

        public async Task<IActionResult> ViewAllLoad()
        {
            try
            {
                var data = await _expenseService.GetAllAsync();
                return PartialView("_ViewAll", data);
            }
            catch(Exception ex)
            {
                throw;
            }
           
           
        }
        
        public async Task<IActionResult> GetAllExpenseCategories()
        {
            List<ExpenseCategory> expenseCategories = (await _expenseCategory.GetAllAsync()).ToList();
            return new JsonResult(expenseCategories);
        }

       
        public async Task<IActionResult> OnGetAddOrEdit(int id = 0)
        {
            try
            {
                ViewData["ExpenseCategories"] = new SelectList(await _expenseCategory.GetAllAsync(), "Id", "Name");
                if (id == 0)
                {
                    
                    //ViewBag.expenseCatlist = new SelectList(expenseCategories, "CategoryId", "CategoryName");
                    //ExpenseViewModel expenseView = new ExpenseViewModel
                    //{
                    //    expenseCategoryItemModels = (await _expenseCategory.GetCategoryItem()).ToList()
                    //};

                    //ViewBag.expenseCatlist = new SelectList(expenseCategoryItemModels).ToList();
                    //List<ExpenseCategoryItemModel> expenseCategories = (await _expenseCategory.GetCategoryItem()).ToList();
                    //ViewBag.expenseCatlist = expenseCategories;
                    //ViewBag.expenseCatlist = new SelectList((await _expenseCategory.GetCategoryItem()).Select(s=> new { CategoryId = s.CategoryId, CategoryName = s.CategoryName }), "CategoryId", "CategoryName");
                    //ViewBag.expenseCatlist = new SelectList((await _expenseCategory.GetAllAsync()).Select(s => new { Id = s.Id, Name = s.Name }), "Id", "Name");
                    return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", new ExpenseViewModel()) });
                }

                else
                {

                    var expense = await _expenseService.GetByIdAsync(id);
                    if (expense == null)
                    {
                        return NotFound();
                    }
                    return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", expense) });
                }

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        [HttpPost]
        public async Task<JsonResult> OnPostAddOrEdit(int id, ExpenseViewModel expenseViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        await _expenseService.AddAsync(expenseViewModel);
                    }
                    else
                    {
                        await _expenseService.UpdateAsync(expenseViewModel);
                    }
                    var expenses = await _expenseService.GetAllAsync();
                    var html = await _renderService.ToStringAsync("_ViewAll", expenses);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    var html = await _renderService.ToStringAsync("_CreateOrEdit", expenseViewModel);
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
            var expense = await _expenseService.SoftDeleteByIdAsync(id);

            if (expense == true)
            {
                var expenses = await _expenseService.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", expenses);
                return new JsonResult(new { isValid = true, html = html });
            }

            else
            {
                return null;
            }
          
        }
    }
}
