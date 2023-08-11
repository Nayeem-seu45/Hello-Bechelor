using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Areas.Admin.Models
{
    public class UserProfileViewModel
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CurrentRole { get; set; } = "Member";
        public string ParmanentAddress { get; set; } = string.Empty;
        public string Gender { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Status { get; set; }
        public bool Smoker { get; set; } = false;
        public string NIDNumber { get; set; }
        public string EmergencyConatctNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
