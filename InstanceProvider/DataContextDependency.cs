
using Microsoft.EntityFrameworkCore;
using FixsyWebApi.Data.UnitOfWork;

namespace FixsyWebApi.InstanceProvider
{
    public static class DataContextDependency{

        public static void AddDataContextDependency(this IServiceCollection services, 
                            string connectionString)
        {
            services.AddDbContext<BCUnitOfWork>(options =>
                 options.UseLazyLoadingProxies()
                        .UseNpgsql(connectionString));
        }
    }
}