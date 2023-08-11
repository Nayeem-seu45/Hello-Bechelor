using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses.Models
{
    public class MemberInfoResponse
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string PermanentAddress { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string NIDNumber { get; set; }
        public int ProfileImageId { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsManager { get; set; }
       
    }
}
