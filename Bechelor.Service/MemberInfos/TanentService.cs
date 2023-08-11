using Bechelor.Core.Common;
using Bechelor.Core.Domin.Bazar;
using Bechelor.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.MemberInfos
{
    public class TanentService : ITenantService
    {
        private readonly IEntityRepository<Tanent> _tenantRepository;

        public TanentService(IEntityRepository<Tanent> tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Tanent> AddAsync(Tanent model)
        {
            return await _tenantRepository.AddAsync(model);
        }

        public async Task<IEnumerable<Tanent>> GetAllAsync()
        {
            var data = await _tenantRepository.GetAllAsync();
            var dataList = data.Where(x => x.IsSoftDeleted == false).ToList();
            return dataList;
        }

        public async Task<Tanent> GetByIdAsync(int id)
        {
            return await _tenantRepository.GetByIdAsync(id);
        }

        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            return await _tenantRepository.SoftDeleteByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Tanent model)
        {
            return await _tenantRepository.UpdateAsync(model);
        }
    }
}
