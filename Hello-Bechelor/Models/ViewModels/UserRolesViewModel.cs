using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsOnline { get; set; }
        
    }
}
