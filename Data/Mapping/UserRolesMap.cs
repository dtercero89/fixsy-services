using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class UserRolesMap : EntityMapBase<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
          
            builder.ToTable("UserRoles", "public"); 

            builder.HasKey(c => c.Id); 

            builder.Property(c => c.Id)
                .IsRequired(); 

            builder.Property(c => c.UserId)
                .IsRequired(); 

            builder.Property(c => c.RoleId)
                .IsRequired(); 
        }
    }
}
