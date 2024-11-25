using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class SupplierMap : EntityMapBase<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {

            builder.ToTable("Suppliers", "public");

            builder.HasKey(r => r.SupplierId);

            builder.Property(r => r.SupplierId)
                .HasColumnName("SupplierId")
                .IsRequired();

            builder.Property(r => r.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(r => r.PhoneNumber)
                 .HasColumnName("PhoneNumber")
                 .HasMaxLength(25);

            builder.Property(r => r.Email)
                 .HasColumnName("Email")
                 .IsRequired();

            builder.Property(r => r.Status)
             .HasColumnName("Status")
             .IsRequired();

        }
    }
}
