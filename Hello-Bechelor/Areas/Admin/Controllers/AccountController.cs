using Bechelor.Core.Common;
using Bechelor.Infrastructure.Extensions;
using Bechelor.Services.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories;
using Bechelor.Services.Expenses.Models;
using Bechelor.Services.MediaFiles;
using Bechelor.Services.MemberInfos;
using Hello_Bechelor.Areas.Admin.Models;
using Hello_Bechelor.Models.ViewModels;
using Hello_Bechelor.ServicesProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hello_Bechelor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : BaseController<AccountController>
    {
        #region field
        private readonly IMemberInfoService _memberInfoService;
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IUserWiseBillService _userWiseBillRepository;
        private readonly IExpenseCategoryService _expenseCategoryService; 
        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IWorkContext _workContext;
        #endregion

        #region ctor
        public AccountController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IWorkContext workContext, IMemberInfoService memberInfoService, IMediaFileRepository mediaFileRepository, IUserWiseBillService userWiseBillRepository, IExpenseCategoryService expenseCategoryService)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _workContext = workContext;
            _memberInfoService = memberInfoService;
            _mediaFileRepository = mediaFileRepository;
            _userWiseBillRepository = userWiseBillRepository;
            _expenseCategoryService = expenseCategoryService;
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
                    if (login.ReturnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
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

            var usercheck = await _userManager.FindByEmailAsync(register.Email ?? "");
            if (usercheck != null)
            {
                ModelState.AddModelError("Email address", "Account already exists. Use differen email!");
            }
            else
            {
                ApplicationUser user = new ();
                MemberInfoResponse member = new();
                UserWiseBillResponse userWiseBillResponse = new();
                SetDataToUser(ref user, ref register);
                SetMemberInfoData(ref member, ref register);
                var memberInfo =   await _memberInfoService.AddAsync(member);
               
                user.MemberInformationId = memberInfo.Id;
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");

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

        private object SetMemberInfoData(ref MemberInfoResponse infoResponse, ref AccountViewModel register)
        {
            infoResponse.Gender = register.Gender;
            infoResponse.JoiningDate = register.JoiningDate;
            infoResponse.Occupation = register.Occupation;
            infoResponse.PermanentAddress = register.PermanentAddress;
            infoResponse.IsManager = false;
            infoResponse.NIDNumber = register.NIDNumber;
            infoResponse.EmergencyContactNumber = register.FamilyContactNumber;
            return infoResponse;
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
                ApplicationUser user = new ()
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
                ApplicationUser user = new ()
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

        public  IActionResult Index()
        {
            return View();
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public string GetRandomPassword()
        {
            const int passwordLength = 8;
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[passwordLength];
                rng.GetBytes(bytes);

                var password = new StringBuilder(passwordLength);

                for (int i = 0; i < passwordLength; i++)
                {
                    password.Append(validChars[bytes[i] % validChars.Length]);
                }

                return password.ToString();
            }
        }

        public async Task<IActionResult> AssignManage(string userId, string role)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(currentUser);
            var allUsers = await _userManager.Users.ToListAsync();
            bool roleIsNotUsed = true;
            if (role is not null)
            {
                await _userManager.RemoveFromRoleAsync(currentUser, roles.FirstOrDefault());
                await _userManager.AddToRoleAsync(currentUser, SD.MessMemberRole);
                var mappedUser = _mapper.Map<List<UserRolesViewModel>>(allUsers);
                 for (int i = 0; i<mappedUser.Count(); i++)
                 {
                    for(int j = i; j<allUsers.Count(); j++)
                    {
                        mappedUser[i].UserId = allUsers[j].Id;
                        mappedUser[i].Roles = await GetUserRoles(allUsers[j]);
                        break;
                    }
                 }
                bool v = mappedUser.Any(x => x.Roles.Equals(SD.ManagerRole));
                if (v)
                {
                    ViewBag.ManagerRoleAssign = SD.Yes;
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.ToStringAsync("_ViewAllUser", mappedUser), message = $"{ SD.ManagerRole } Role Assign Successfully." });
            }
            else
            {
                foreach (var user in allUsers)
                {
                    if (await _userManager.IsInRoleAsync(user, SD.ManagerRole))
                    {
                        roleIsNotUsed = false;
                    }
                }
                if (roleIsNotUsed)
                {
                    await _userManager.RemoveFromRoleAsync(currentUser, roles.FirstOrDefault());
                    await _userManager.AddToRoleAsync(currentUser, SD.ManagerRole);
                    //return View(nameof(Index));
                    var mappedUser = _mapper.Map<List<UserRolesViewModel>>(allUsers);

                    for (int i = 0; i < mappedUser.Count(); i++)
                    {
                        for (int j = i; j < allUsers.Count(); j++)
                        {
                            mappedUser[i].UserId = allUsers[j].Id;
                            mappedUser[i].Roles = await GetUserRoles(allUsers[j]);
                            break;
                        }
                    }
                    bool v = mappedUser.Any(x => x.Roles.Equals(SD.ManagerRole));
                    if (v)
                    {
                        ViewBag.ManagerRoleAssign = SD.Yes;
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.ToStringAsync("_ViewAllUser", mappedUser), message = $"{ SD.ManagerRole } Role Assign Successfully." });

                }
                else
                {
                    Notify("Role is being Used by another User. Cannot Delete.");
                    return View();
                }
            }
        }

        public async Task<IActionResult> LoadAll()
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
                    UserId = item.Id,
                    IsOnline = item.IsOnline,
                    Roles = await GetUserRoles(item)
                };

                userRolesViewModel.Add(model);
            }

            var v = userRolesViewModel.Where(x => x.Roles.Any(i => i.Equals(SD.ManagerRole)));
            userRolesViewModel.RemoveAll(i => i.Email == "Admin@gmail.com");

            if (v.Count()>0)
            {
               ViewBag.ManagerRoleAssign = SD.Yes;
            }
            return PartialView("_ViewAllUser", userRolesViewModel);
        }

        public async Task<IActionResult> Profile(string UserId, UserProfileViewModel profileModel)
        {
            var currentUser = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(currentUser);
            var memberInfo = await _memberInfoService.GetAllAsync();
            var imageUrl = await _mediaFileRepository.GetAllAsync();
            var memberData =  memberInfo.Where(d => d.Id == currentUser.MemberInformationId);
            var imagePath = imageUrl.Where(s => s.Id == memberData.FirstOrDefault().ProfileImageId);
            if(memberData != null)
            {
                profileModel.Gender = memberData.FirstOrDefault().Gender;
                profileModel.ParmanentAddress = memberData.FirstOrDefault().PermanentAddress;
                profileModel.JoiningDate = memberData.FirstOrDefault().JoiningDate;
                profileModel.NIDNumber = memberData.FirstOrDefault().NIDNumber;
            }
            if(imagePath.Count() >0)
            {
                profileModel.ProfilePictureUrl = imagePath.FirstOrDefault().FileUrl;
            }
            profileModel.CurrentRole = roles.FirstOrDefault();
            profileModel.FullName = currentUser.FullName;
            profileModel.Email = currentUser.Email;
            profileModel.PhoneNumber = currentUser.PhoneNumber;
           
            return View(profileModel);
        }
        #endregion
    }
}
