using Bechelor.Core.Common;
using Bechelor.Infrastructure.Data;
using Bechelor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bechelor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BechelorContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("constring"));
                option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);
            services.AddIdentity<ApplicationUser, ApplicationRole>(
             options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.SignIn.RequireConfirmedEmail = false;
             }).AddEntityFrameworkStores<BechelorContext>().AddDefaultTokenProviders();


            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IWorkContext, WorkContext>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
