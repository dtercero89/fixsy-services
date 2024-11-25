using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class UserMap : EntityMapBase<Agg.User>
    {
        public void Configure(EntityTypeBuilder<Agg.User> builder)
        {
      
            builder.ToTable("Users", "public"); 

            builder.HasKey(c => c.UserId); 

            builder.Property(c => c.UserId)
                .IsRequired(); 

            builder.Property(c => c.Name)
                .IsRequired() 
                .HasMaxLength(500); 

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20); 

            builder.Property(c => c.Email)
                .HasMaxLength(100); 

            builder.Property(c => c.Photo)
                .HasColumnType("bytea"); 

            builder.Property(c => c.Password)
                .IsRequired() 
                .HasMaxLength(300); 
        }
    }
}
