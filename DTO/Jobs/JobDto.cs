namespace FixsyWebApi.DTO.Jobs
{
    public class JobDto:ResponseBase
    {
        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreferredSchedule { get; set; }
        public string Requirements { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? AssignedAt { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int SupplierId { get; set; }

        public string SupplierEmail { get; set; }
        public string SupplierPhoneNumber { get; set; }

        public int TotalCount { get; set; }
    }
}
