using System.ComponentModel.DataAnnotations;

namespace FixsyWebApi.Data.Agg
{
    public class Job : EntityBase
    {
        [Key]
        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreferredSchedule { get; set; }
        public string Requirements { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; }

        public virtual ICollection<JobAssignment> JobAssignments { get; private set; }

        internal void Complete()
        {
            Status = Resources.Status.Completed;
        }

        internal bool IsCompleted()
        {
            return Status == Resources.Status.Completed;
        }

        public class Builder
        {
            private Job _job = new();

            public Builder WithJob(string title, string description, string preferredSchedul, DateTime createdAt, string status)
            {
                _job.Title = title;
                _job.Description = description;
                _job.PreferredSchedule = preferredSchedul;
                _job.CreatedAt = createdAt; 
                _job.Status = status;
                
                return this;
            }

            public Builder WithService(int serviceId, string requirements) {

                _job.ServiceId = serviceId;
                _job.Requirements = requirements;
                return this;
            }

            public Builder WithCustomer(int customerId) {

                _job.CustomerId = customerId;
                return this;
            }

            public Job Build()
            {
                return _job;
            }
        }

    }
}
