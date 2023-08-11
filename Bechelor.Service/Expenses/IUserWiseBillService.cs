using Bechelor.Core.Domin.Core;
using Bechelor.Services.Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses
{
    public interface IUserWiseBillService 
    {
        Task<IEnumerable<UserWiseBill>> GetAllAsync();
        Task<UserWiseBill> GetByIdAsync(int id);
        Task<UserWiseBill> AddAsync(UserWiseBillResponse model);
        Task<bool> UpdateAsync(UserWiseBillResponse model);
        Task<int> TotalCountAsync();
        Task<bool> SoftDeleteByIdAsync(int id);
    }
}
