using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Models.ViewModels
{
    public class AccountViewModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string ReturnUrl { get; set; }
        public string IncorrectInput { get; set; }
        public bool RememberMe { get; set; }
        public string  NickName { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string FamilyContactNumber { get; set; }
        public string PermanentAddress { get; set; }
        public DateTime JoiningDate { get; set; }
        public string ProfileImageUrl { get; set; }
        public string NIDNumber  { get; set; }
        public string Email  { get; set; }
    }
}
