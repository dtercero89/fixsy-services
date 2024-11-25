using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class JobMap : EntityMapBase<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {

            builder.ToTable("Jobs", "public");

            builder.HasKey(r => r.JobId);

            builder.Property(r => r.JobId)
                .HasColumnName("JobId")
                .IsRequired();

            builder.Property(r => r.ServiceId)
            .HasColumnName("ServiceId")
            .IsRequired();

            builder.Property(r => r.Status)
            .HasColumnName("Status").IsRequired();         

            builder.Property(r => r.CustomerId)
                    .HasColumnName("CustomerId")
                    .IsRequired();

            builder.Property(r => r.Title)
                  .HasColumnName("Title")
                  .IsRequired();

            builder.Property(r => r.Description)
                  .HasColumnName("Description")
                  .IsRequired();

            builder.Property(r => r.PreferredSchedule)
                .HasColumnName("PreferredSchedule");


            builder.Property(r => r.Requirements)
                .HasColumnName("Requirements");


            builder.Property(r => r.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp without time zone");

            

        }
    }
}
