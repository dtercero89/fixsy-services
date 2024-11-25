using FixsyWebApi.Data;
using FixsyWebApi.Data.Repository;
using FixsyWebApi.Data.UnitOfWork;

namespace FixsyWebApi.InstanceProvider
{
      public static class UnitOfWorkDependecy{

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IFixsyDataContext, FixsyUnitOfWork>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericRepository<IFixsyDataContext>), typeof(GenericRepository<IFixsyDataContext>));
        }
    }
}