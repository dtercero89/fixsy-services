using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FixsyWebApi.Data.Agg;

namespace FixsyWebApi.Data.Mapping
{
    public class EntityMapBase<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : EntityBase
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
    
            builder.Property(c => c.ModifiedBy)
                   .HasMaxLength(10); 

            builder.Property(c => c.TransactionDate)
                   .HasConversion(
                       v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                       v => DateTime.SpecifyKind(v, DateTimeKind.Utc) 
                   );
        }
    }
}
