using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class RoleMap : EntityMapBase<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
       
            builder.ToTable("Roles", "public");

            builder.HasKey(r => r.RoleId); 

            builder.Property(r => r.RoleId)
                .HasColumnName("RoleId") 
                .IsRequired(); 

            builder.Property(r => r.Name)
                .HasColumnName("Name") 
                .IsRequired() 
                .HasMaxLength(255); 

        }
    }
}
