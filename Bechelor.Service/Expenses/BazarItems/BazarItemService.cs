using AutoMapper;
using Bechelor.Core.Domin.Bazar;
using Bechelor.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bechelor.Services.Expenses.BazarItems
{
    public class BazarItemService : IBazarItemService
    {
        private readonly IEntityRepository<BazarItem> _bazarItemRepository;
        private readonly IMapper _mapper;

        public BazarItemService(IEntityRepository<BazarItem> bazarItemRepository, IMapper mapper)
        {
            _bazarItemRepository = bazarItemRepository;
            _mapper = mapper;
        }
        public async Task<BazarItem> AddAsync(BazarItem model)
        {
            return await _bazarItemRepository.AddAsync(model);
        }

        public async Task<IEnumerable<BazarItem>> GetAllAsync()
        {
            var data = await _bazarItemRepository.GetAllAsync();
            var dataList = data.Where(x => x.IsSoftDeleted == false).ToList();
            return dataList;
        }

        public async Task<BazarItem> GetByIdAsync(int id)
        {
            return await _bazarItemRepository.GetByIdAsync(id);
        }
        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            return await _bazarItemRepository.SoftDeleteByIdAsync(id);
        } 
        public async Task<bool> UpdateAsync(BazarItem model)
        {
            return await _bazarItemRepository.UpdateAsync(model);
        }
    }
}
