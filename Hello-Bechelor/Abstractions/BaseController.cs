using AutoMapper;
using Core.MSSQL.SqlHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Hello_Bechelor.Abstractions
{
    public class BaseController<T> : Controller
    {
        private IMapper _mapperInstance;
        private ISqlHelperService _sqlHelperService;

        protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
        protected ISqlHelperService _sqlHelper => _sqlHelperService ??= HttpContext.RequestServices.GetService<ISqlHelperService>();
    }
}
