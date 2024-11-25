namespace FixsyWebApi.DTO.Jobs
{
    public class JobAssignmentDto : ResponseBase
    {
        public int AssignmentId { get; set; }
        public int JobId { get; set; }
        public int SupplierId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
