using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.Commons
{
    public interface ICommonService
    {
        Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env);
    }
}
