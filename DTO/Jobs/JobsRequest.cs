using FixsyWebApi.Data.UnitOfWork;

namespace FixsyWebApi.DTO.Jobs
{
    public class PublichJobRequest : RequestBase
    {
        public JobDto Job { get; set; }
    }

    public class AssignJobRequest : RequestBase
    {
        public int JobId { get; set; }
        public int SupplierId { get; set; }
        public DateTime AssignedAt { get; set; }
    }

    public class GetJobsRequest : PagedRequestBase
    {
        public string Status { get; set; }
    }

    public class CompleteJobRequest : PagedRequestBase
    {
        public int JobId { get; set; }

    }

    public class UpdateJobRequest : PagedRequestBase
    {
        public int SupplierId { get; set; }
        public string Notes { get; set; }
        public int JobId { get; set; }
        public DateTime? AssignedAt { get; set; }

    }

    public class GetJobByIdRequest:RequestBase 
    {
        public int JobId { get; set; }
    }

    public class GetJobsSummaryRequest: RequestBase
    {
        
    }
}
