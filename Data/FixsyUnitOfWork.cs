using Microsoft.EntityFrameworkCore;
using FixsyWebApi.Data.Agg;
using FixsyWebApi.Data.Mapping;
using FixsyWebApi.Data.UnitOfWork;

namespace FixsyWebApi.Data
{

    public class FixsyUnitOfWork : BCUnitOfWork, IFixsyDataContext
    {
        public FixsyUnitOfWork(DbContextOptions<BCUnitOfWork> options) :
            base(options)
        {

        }

        public DbSet<Role> Role { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Agg.User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Agg.Permission> Permissions { get; set; }
        public DbSet<PermissionByRole> PermissionByRole { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAssignment> JobAssignments { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierServices> SupplierServices { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PermissionMap());
            modelBuilder.ApplyConfiguration(new UserRolesMap());
            modelBuilder.ApplyConfiguration(new PermissionByRoleMap());
            modelBuilder.ApplyConfiguration(new ServiceMap());
            modelBuilder.ApplyConfiguration(new JobMap());
            modelBuilder.ApplyConfiguration(new JobAssignmentMap());
            modelBuilder.ApplyConfiguration(new SupplierMap());
            modelBuilder.ApplyConfiguration(new SupplierServicesMap());
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            Database.SetCommandTimeout(10000);
            return Database.SqlQueryRaw<TEntity>(sqlQuery, parameters);
        }

    }
}