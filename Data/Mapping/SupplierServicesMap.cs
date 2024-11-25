using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class SupplierServicesMap : EntityMapBase<SupplierServices>
    {
        public void Configure(EntityTypeBuilder<SupplierServices> builder)
        {

            builder.ToTable("SupplierServices", "public");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(r => r.SupplierId)
                .HasColumnName("SupplierId")
                .IsRequired();

            builder.Property(r => r.ServiceId)
                .HasColumnName("ServiceId")
                .IsRequired();

            builder.HasOne(r => r.Service)
                .WithMany(s => s.SupplierServices)
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
