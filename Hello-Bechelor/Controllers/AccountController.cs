using Bechelor.Core.Common;
using Bechelor.Infrastructure.Extensions;
using Hello_Bechelor.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hello_Bechelor.Controllers
{
    public class AccountController : Controller
    {
        #region field
        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IWorkContext _workContext;
        #endregion

        #region ctor
        public AccountController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IWorkContext workContext)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _workContext = workContext;
        }
        #endregion

        #region method
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            if (_signInManager.IsSignedIn(User))
            {

                return RedirectToAction("Index", "Home");

            }
           
                //return RedirectToAction("action", "controller", new { area = "area" });
          
            returnUrl = returnUrl.Replace("%2F", "/");
            var model = new AccountViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountViewModel login)
        {
            string phonenumber = login.PhoneNumber;
            var isValid = IsValidEmail(login.PhoneNumber);
            if (isValid == false)
            {
                string mail = login.PhoneNumber + "@ecommerce.com";
                phonenumber = mail.ToString();
            }

            var user = await _userManager.FindByEmailAsync(phonenumber);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (result.Succeeded)
                {
                    //ApplicationUser userData = new();
                    user.IsOnline = true;
                    await _userManager.UpdateAsync(user);

                    if (login.ReturnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    if(user.Email == "admin@gmail.com")
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    return Redirect(login.ReturnUrl);
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("IncorrectInput", "Email is not verified.");
                    return View(login);
                }
            }
            ModelState.AddModelError("IncorrectInput", "Username or Password is incorrect");
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountViewModel register)
        {
            //if (isValid == false)
            //{
            //    ModelState.AddModelError("Email", "Email is not valid!");
            //}

            var usercheck = await _userManager.FindByNameAsync(register.FullName ?? "");
            if (usercheck != null)
            {
                ModelState.AddModelError("PhoneNumber", "Account already exists. Use different phone number or email!");
            }
            else
            {
                ApplicationUser user = new ApplicationUser();
                SetDataToUser(ref user, ref register);
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "client");

                    var signres = await _signInManager.PasswordSignInAsync(user, register.Password, false, false);
                    if (signres.Succeeded)
                    {
                        if (register.ReturnUrl == null)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        return Redirect(register.ReturnUrl);
                    }
                    if (signres.IsNotAllowed)
                    {
                        ModelState.AddModelError("IncorrectInput", "Email is not verified.");
                        return View(nameof(Login));
                    }
                    return Redirect("/Home/Index");
                }
            }

            return View(register);
        }
        private object SetDataToUser(ref ApplicationUser user, ref AccountViewModel register)
        {
            string phonenumber = register.PhoneNumber;
            var isValid = IsValidEmail(register.PhoneNumber);
            if (isValid == false)
            {
                string mail = register.PhoneNumber + "@bechelor.com";
                phonenumber = mail.ToString();
                user.PhoneNumber = register.PhoneNumber;
            }
            user.Email = phonenumber;
            user.UserName = register.PhoneNumber;
            user.FullName = register.FullName;
            return user;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            return Redirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogoutAction()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("google-login")]
        public IActionResult GoogleLogin(string returnUrl = null)
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse(string returnUrl = null)
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(GoogleLogin));

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Name).Value,
                    PhoneNumber = info.Principal.FindFirst(ClaimTypes.MobilePhone).Value
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                    }
                }
                //return AccessDenied();
                return View("Login");
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult FacebookLogin(string returnUrl = null)
        {
            var redirectUrl = Url.Action("FacebookResponse", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> FacebookResponse(string returnUrl = null)
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(FacebookLogin));

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Name).Value,
                    PhoneNumber = info.Principal.FindFirst(ClaimTypes.MobilePhone).Value
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                    }
                }
                //return AccessDenied();
                return View("Login");
            }
        }

        public IActionResult GoogleLogInSuccess()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var userlist = await _userManager.Users.ToListAsync();
            List<UserRolesViewModel> userRolesViewModel = new List<UserRolesViewModel>();
            foreach (var item in userlist)
            {
                UserRolesViewModel model = new UserRolesViewModel()
                {
                    FullName = item.FullName,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    Roles = await GetUserRoles(item)
                };

                userRolesViewModel.Add(model);
            }
            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        #endregion
    }
}
