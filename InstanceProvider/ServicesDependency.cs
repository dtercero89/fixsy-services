using FixsyWebApi.Data.Helper;
using FixsyWebApi.Data.Identity;
using FixsyWebApi.Services;
using FixsyWebApi.Services.Customer;
using FixsyWebApi.Services.Jobs;
using FixsyWebApi.Services.Service;
using FixsyWebApi.Services.Suppliers;
using FixsyWebApi.Services.Users;

namespace FixsyWebApi.InstanceProvider
{
    public static class ServicesDependency{

        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddTransient<CustomersAppService>();
            services.AddTransient<ProfileAppService>();
            services.AddTransient<UsersAppService>();
            services.AddTransient<ServicesAppService>();
            services.AddTransient<SuppliersAppServices>();
            services.AddTransient<JobsAppService>();


            services.AddTransient<IIdentityGenerator,IdentityGenerator>();
            services.AddScoped<PostgreSqlQueryExecutor>();
        }
    }
}