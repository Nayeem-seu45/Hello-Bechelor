using Bechelor.Core.Domin.Core;
using Bechelor.Infrastructure.Data;
using Bechelor.Services.Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses
{
    public record UserWiseBillService(IEntityRepository<UserWiseBill> _userWiseRepository) : IUserWiseBillService
    {
        public async Task<UserWiseBill> AddAsync(UserWiseBillResponse model)
        {
            UserWiseBill userWiseBill = AddOrUpdateMappingProperties(ref model);
            return await _userWiseRepository.AddAsync(userWiseBill);
        }

        public async Task<IEnumerable<UserWiseBill>> GetAllAsync()
        {
            return await _userWiseRepository.GetAllAsync();
        }

        public async Task<UserWiseBill> GetByIdAsync(int id)
        {
            return await _userWiseRepository.GetByIdAsync(id);
        }

        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            return await _userWiseRepository.SoftDeleteByIdAsync(id);
        }

        public Task<int> TotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(UserWiseBillResponse model)
        {
            UserWiseBill userWiseBill = AddOrUpdateMappingProperties(ref model);

            return await _userWiseRepository.UpdateAsync(userWiseBill);
        }

        private UserWiseBill AddOrUpdateMappingProperties(ref UserWiseBillResponse model)
        {
            UserWiseBill userWiseBill = new()
            {
                UserId = model.UserId,
                ExpenseCategoryId = model.ExpenseCategoryId,
                Amount = model.Amount,
                CreatedOn = DateTime.Now
            };
            return userWiseBill;
        }
    }
}
