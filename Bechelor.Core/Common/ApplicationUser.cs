using Bechelor.Core.Domin.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Common
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string NickName { get; set; }
        public int MemberInformationId { get; set; }
        public bool IsOnline { get; set; } = false;
        public virtual MemberInformation MemberInformation { get; set; }
    }
}
