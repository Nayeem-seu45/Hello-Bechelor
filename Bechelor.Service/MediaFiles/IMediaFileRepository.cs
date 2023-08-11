using Bechelor.Core.Domin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.MediaFiles
{
    public interface IMediaFileRepository
    {
        Task<IEnumerable<MediaFile>> GetAllAsync();
        Task<MediaFile> GetByIdAsync(int id);
        Task<MediaFile> AddAsync(MediaFile model);
        Task<bool> UpdateAsync(MediaFile model);
        Task<int> TotalCountAsync();
        Task<bool> SoftDeleteByIdAsync(int id);
    }
}
