using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FixsyWebApi.Data.Agg;

namespace FixsyWebApi.Data.Mapping
{
    public class PermissionMap : EntityMapBase<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", "public"); 

            builder.HasKey(p => p.PermissionId);

            builder.Property(p => p.PermissionId)
                .HasColumnName("PermissionId")
                .IsRequired(); 

            builder.Property(p => p.Name)
                .HasColumnName("Name") 
                .IsRequired() 
                .HasMaxLength(255); 

      
        }
    }
}
