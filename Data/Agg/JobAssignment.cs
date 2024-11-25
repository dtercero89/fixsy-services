using System.ComponentModel.DataAnnotations;

namespace FixsyWebApi.Data.Agg
{
    public class JobAssignment : EntityBase
    {

        [Key]
        public int AssignmentId { get; set; }
        public int JobId { get; set; }
        public int SupplierId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        
        public string Notes { get; set; }

        public virtual Job Job { get; set; }
    }
}
