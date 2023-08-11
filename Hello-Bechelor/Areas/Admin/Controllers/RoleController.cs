using Bechelor.Core.Common;
using Hello_Bechelor.Areas.Admin.Models;
using Hello_Bechelor.ServicesProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Hello_Bechelor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : BaseController<RoleController>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            //List<RoleViewModel> model = new List<RoleViewModel>();
            //model = _roleManager.Roles.Select(r => new RoleViewModel
            //{
            //    Name = r.Name,
            //    Id = r.Id,
            //    Description = r.Description,
            //    NumberOfUsers = _userManager.GetUsersInRoleAsync(r.Name).Result.Count()
            //}).ToList();
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var model = _mapper.Map<List<RoleViewModel>>(roles);
            for(int i=0; i<model.Count(); i++)
            {
                var count = _userManager.GetUsersInRoleAsync(model[i].Name).Result.Count();
                model[i].NumberOfUsers = count;
            }
            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> OnGetCreate(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new JsonResult(new { isValid = true, html = await _viewRenderer.ToStringAsync("_CreateOrEdit", new RoleViewModel()) });
            else
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return new  CustomMessageResult("Unexpected Error.Role not found!");
                }
                else
                {
                    var roleviewModel = _mapper.Map<RoleViewModel>(role);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.ToStringAsync("_CreateOrEdit", roleviewModel) });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreate(RoleViewModel role)
        {
            if (ModelState.IsValid && role.Name != "SuperAdmin" && role.Name != "Basic")
            {
                if (string.IsNullOrEmpty(role.Id))
                {
                    bool isExist = !String.IsNullOrEmpty(role.Id);
                    ApplicationRole applicationRole = isExist ? await _roleManager.FindByIdAsync(role.Id) :
                   new ()
                   {
                       CreatedDate = DateTime.UtcNow,
                       Name = role.Name,
                       Description  = role.Description
                   };
                    await _roleManager.CreateAsync(applicationRole);
                   // _notify.Success("Role Created");
                }
                else
                {
                    var existingRole = await _roleManager.FindByIdAsync(role.Id);
                    existingRole.Name = role.Name;
                    existingRole.Description = role.Description;
                    existingRole.NormalizedName = role.Name.ToUpper();
                    await _roleManager.UpdateAsync(existingRole);
                    //_notify.Success("Role Updated");
                }

                var roles = await _roleManager.Roles.ToListAsync();
                var mappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(roles);
                var html = await _viewRenderer.ToStringAsync("_ViewAll", mappedRoles);
                return new JsonResult(new { isValid = true, html = html, message = "Update data success." });
            }
            else
            {
                var html = await _viewRenderer.ToStringAsync<RoleViewModel>("_CreateOrEdit", role);
                return new JsonResult(new { isValid = false, html = html, message = "Add new data success." });
            }
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            var existingRole = await _roleManager.FindByIdAsync(id);
            if (existingRole.Name != "admin" && existingRole.Name != "Basic")
            {
                //TODO Check if Any Users already uses this Role
                bool roleIsNotUsed = true;
                var allUsers = await _userManager.Users.ToListAsync();
                foreach (var user in allUsers)
                {
                    if (await _userManager.IsInRoleAsync(user, existingRole.Name))
                    {
                        roleIsNotUsed = false;
                    }
                }
                if (roleIsNotUsed)
                {
                    await _roleManager.DeleteAsync(existingRole);
                    Notify($"Role {existingRole.Name} deleted.");
                  
                }
                else
                {
                    Notify("Role is being Used by another User. Cannot Delete.");
                }
            }
            else
            {
                Notify($"Not allowed to  delete {existingRole.Name} Role.");
            }
            var roles = await _roleManager.Roles.ToListAsync();
            var mappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(roles);
            var html = await _viewRenderer.ToStringAsync("_ViewAll", mappedRoles);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
