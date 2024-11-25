using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class ServiceMap : EntityMapBase<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services", "public");

            builder.HasKey(p => p.ServiceId); 

            builder.Property(p => p.ServiceId)
                .HasColumnName("ServiceId") 
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired();
        }
    }
}
