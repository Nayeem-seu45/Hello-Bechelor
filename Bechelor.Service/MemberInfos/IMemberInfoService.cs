using Bechelor.Core.Domin.Core;
using Bechelor.Services.Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.MemberInfos
{
    public interface IMemberInfoService
    {
        Task<IEnumerable<MemberInfoResponse>> GetAllAsync();
        Task<MemberInfoResponse> GetByIdAsync(int id);
        Task<MemberInformation> AddAsync(MemberInfoResponse model);
        Task<bool> UpdateAsync(MemberInfoResponse model);
        Task<int> TotalCountAsync();
        Task<bool> SoftDeleteByIdAsync(int id);
    }
}
