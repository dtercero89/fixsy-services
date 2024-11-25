using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class PermissionByRoleMap : EntityMapBase<PermissionByRole>
    {
        protected void Configure(EntityTypeBuilder<PermissionByRole> builder)
        {
            builder.ToTable("PermissionsByRole", "public");

            builder.HasKey(p => p.Id); 

            builder.Property(p => p.PermissionId)
                .HasColumnName("PermissionId") 
                .IsRequired(); 

            builder.Property(p => p.RoleId)
                .HasColumnName("RoleId") 
                .IsRequired(); 
        }
    }
}
