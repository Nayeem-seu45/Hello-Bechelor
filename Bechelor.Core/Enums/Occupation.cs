using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Enums
{
    public enum Occupation
    {
        [Display(Name ="Job Holder")]
        Job_Holder = 1,
        Student = 2

    }
}
