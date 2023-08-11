using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.MemberInfos
{
    public interface ITenantService
    {
        Task<IEnumerable<Tanent>> GetAllAsync();
        Task<Tanent> GetByIdAsync(int id);
        Task<Tanent> AddAsync(Tanent model);
        Task<bool> UpdateAsync(Tanent model);
        Task<bool> SoftDeleteByIdAsync(int id);
    }
}
