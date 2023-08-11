using Bechelor.Core.Domin.Core;
using Bechelor.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.MediaFiles
{
    public record MediaFileService : IMediaFileRepository
    {
        private readonly IEntityRepository<MediaFile> _mediaFileRepository;

        public MediaFileService(IEntityRepository<MediaFile> entityRepository)
        {
            _mediaFileRepository = entityRepository;
        }
        public async Task<MediaFile> AddAsync(MediaFile model)
        {
            return await _mediaFileRepository.AddAsync(model);
        }

        public async Task<IEnumerable<MediaFile>> GetAllAsync()
        {
            return await _mediaFileRepository.GetAllAsync();
        }

        public async Task<MediaFile> GetByIdAsync(int id)
        {
            return await _mediaFileRepository.GetByIdAsync(id);
        }

        public async Task<bool> SoftDeleteByIdAsync(int id)
        {
            return await _mediaFileRepository.SoftDeleteByIdAsync(id);
        }

        public Task<int> TotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(MediaFile model)
        {
            return await _mediaFileRepository.UpdateAsync(model);
        }
    }
}
