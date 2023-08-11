using Bechelor.Core.Common;
using Hello_Bechelor.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Views.Shared.Components.Users
{
    public class UsersViewComponent : ViewComponent
    {
        private UserManager<ApplicationUser> _userManager;
        public UsersViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync( string selectedId,string fullName, string lableClass, bool isRequired = true)
        {
            ViewBag.SelectedId = selectedId;
            ViewBag.User = fullName;
            ViewBag.LableClass = lableClass;
            ViewBag.IsRequired = isRequired;
            ViewBag.UserList = await GetAllUser();
            return View();
        }

        private async Task<List<ApplicationUser>> GetAllUser()
        {
           List<ApplicationUser> usersList = await _userManager.Users.ToListAsync();
            return usersList;
        }
    }
}
