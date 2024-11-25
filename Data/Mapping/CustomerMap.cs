using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FixsyWebApi.Data.Agg;

namespace FixsyWebApi.Data.Mapping
{
    internal class CustomerMap : EntityMapBase<Customer>
    {
        protected void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "public");
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.Property(c => c.Address)
                .HasColumnName("Address") 
                .IsRequired();

            builder.HasOne(r => r.User)
                    .WithMany(s => s.Customers)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
