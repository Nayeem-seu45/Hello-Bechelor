using Bechelor.Services.Commons;
using Bechelor.Services.Expenses;
using Bechelor.Services.Expenses.ExpenseCategories;
using Bechelor.Services.MediaFiles;
using Bechelor.Services.MemberInfos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddTransient<IMemberInfoService, MemberInfoService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IMediaFileRepository, MediaFileService>();
            services.AddTransient<IUserWiseBillService, UserWiseBillService>();
            services.AddTransient<ITenantService, TanentService>();
            return services;
        }
    }
}
