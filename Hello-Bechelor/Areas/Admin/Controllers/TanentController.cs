using Bechelor.Services.Expenses.ExpenseCategories;
using Bechelor.Services.MemberInfos;
using Hello_Bechelor.Areas.Admin.Models;
using Hello_Bechelor.ServicesProvider;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Bechelor.Core.Common;
using Bechelor.Services.Expenses.Models;

namespace Hello_Bechelor.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class TanentController : BaseController<TanentController>
    {
        private readonly ITenantService _tenantService;
        private readonly IRazorRenderService _renderService;

        public TanentController(ITenantService tenantService, IRazorRenderService renderService)
        {
            _tenantService = tenantService;
            _renderService = renderService;
        }
        public IActionResult Index()
        {
            var model = new TanentViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _tenantService.GetAllAsync();
            var viewModel = _mapper.Map<List<TanentViewModel>>(response);
            return PartialView("_ViewAll", viewModel);
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var lookupViewModel = new TanentViewModel();
                    return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", lookupViewModel) });
                }
                else
                {
                    var response = await _tenantService.GetByIdAsync(id);
                    if (response is not null)
                    {
                        var tenantViewModel = _mapper.Map<TanentViewModel>(response);
                        return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", tenantViewModel) });
                    }
                    return new JsonResult(new { isValid = false });
                }
            }
            catch(Exception ex)
            {
                return new JsonResult(new { isValid = false });
            }
            
        }


        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, TanentViewModel tanent)
        {
            try
            {
                var tanentData = _mapper.Map<Tanent>(tanent);
                if (id == 0)
                {
                    var result = await _tenantService.AddAsync(tanentData);

                    var tanentlist = await _tenantService.GetAllAsync();
                    if (result.Id != 0)
                    {
                        var html = await _renderService.ToStringAsync("_ViewAll", tanentlist);
                        return new JsonResult(new { isValid = true, html, message = "Add new data success." });
                    }
                    else
                    {
                        var html = await _renderService.ToStringAsync("_ViewAll", tanentlist);
                        return new JsonResult(new { isValid = false, html, message = "Somthing Wrong" });
                    }
                }
                else
                {
                    await _tenantService.UpdateAsync(tanentData);
                    var tenantList = await _tenantService.GetAllAsync();
                    var html = await _renderService.ToStringAsync("_ViewAll", tenantList);
                    return new JsonResult(new { isValid = true, html, message = "Edit data success." });

                }
            }
            catch (Exception ex)
            {
                var html = await _renderService.ToStringAsync("_CreateOrEdit", tanent);
                return new JsonResult(new { isValid = false, html, message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var tanent = await _tenantService.SoftDeleteByIdAsync(id);

            if (tanent == true)
            {
                var tanentList = await _tenantService.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", tanentList);
                return new JsonResult(new { isValid = true, html });
            }

            else
            {
                return null;
            }

        }



    }
}
