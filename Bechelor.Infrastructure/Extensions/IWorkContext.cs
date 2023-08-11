using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Infrastructure.Extensions
{
    public interface IWorkContext
    {
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<bool> IsUserSignedIn();
    }
}
