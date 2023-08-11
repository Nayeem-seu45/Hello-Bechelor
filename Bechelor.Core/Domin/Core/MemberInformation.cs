using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Domin.Core
{
    public class MemberInformation : BaseEntity
    {
        [MaxLength(40)]
        public string Occupation { get; set; }
        [MaxLength(20)]
        public  string Gender { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string NIDNumber { get; set; }
        public int ProfileImageId { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsManager { get; set; }
        [MaxLength(200)]
        public string PermanentAddress { get; set; }
    }
}
