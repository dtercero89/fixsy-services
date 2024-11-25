using FixsyWebApi.Data.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixsyWebApi.Data.Mapping
{
    public class JobAssignmentMap : EntityMapBase<JobAssignment>
    {
        public void Configure(EntityTypeBuilder<JobAssignment> builder)
        {

            builder.ToTable("JobAssignments", "public");

            builder.HasKey(r => r.AssignmentId);

            builder.Property(r => r.AssignmentId)
                .HasColumnName("AssignmentId")
                .IsRequired();

            builder.Property(r => r.JobId)
                .HasColumnName("JobId")
                .IsRequired();

            builder.Property(r => r.SupplierId)
                  .HasColumnName("SupplierId")
                  .IsRequired();

            builder.Property(r => r.AssignedAt)
            .HasColumnName("AssignedAt")
            .HasColumnType("timestamp without time zone");


            builder.Property(r => r.CompletedAt)
            .HasColumnName("CompletedAt")
            .HasColumnType("timestamp without time zone");

 

            builder.Property(r => r.Notes)
            .HasColumnName("Notes");

            builder.HasOne(r => r.Job)
            .WithMany(s => s.JobAssignments)
            .HasForeignKey(r => r.JobId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
