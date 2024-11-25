using FixsyWebApi.Data.Repository;
using FixsyWebApi.Data;
using FixsyWebApi.DTO.Jobs;
using FixsyWebApi.Data.Agg;
using FixsyWebApi.Data.Extensions;
using FixsyWebApi.Resources;
using FixsyWebApi.DTO;
using FixsyWebApi.Data.Helper;
using Castle.Core.Resource;

namespace FixsyWebApi.Services.Jobs
{
    public class JobsAppService
    {
        private readonly IGenericRepository<IFixsyDataContext> _repository;
        private readonly PostgreSqlQueryExecutor _sqlQueryExecutor;

        public JobsAppService(IGenericRepository<IFixsyDataContext> repository,
            PostgreSqlQueryExecutor sqlQueryExecutor)
        {
            _repository = repository;
            _sqlQueryExecutor = sqlQueryExecutor;
        }

        public async Task<JobDto> AssignJobToSupplier(AssignJobRequest request)
        {
            var existingJob = await _repository.GetSingleAsync<Job>(s=>s.JobId == request.JobId);

            if (existingJob.IsNull()) {
                return new JobDto { ValidationErrorMessage = Messages.MustSelectAJob };
            }

            var existingSupplier = await _repository.GetSingleAsync<Supplier>(s=>s.SupplierId == request.SupplierId);
            if (existingSupplier.IsNull()) {

                return new JobDto { ValidationErrorMessage = Messages.MustSelectSupplier };
            }

            var assignment = new JobAssignment
            {
                JobId = request.JobId,
                SupplierId = request.SupplierId,
                AssignedAt = request.AssignedAt,
            };

            _repository.Add(assignment);

            _repository.UnitOfWork.Commit(request.GetTransactionInfo(Transactions.AssignJob));

            return new JobDto { SuccessMessage = Messages.SuccessfullyJobAssignment.AddParameters(existingSupplier.Name) };
        }

        public async Task<PagedResponse<JobDto>> GetJobsPaged(GetJobsRequest request)
        {
            var parameters = new
            {
                PageNumber = request.Page,
                PageSize = request.PageSize,
                SearchValue = request.SearchValue,
                Status = request.Status,
                CustomerId = request.CustomerId.GetValueOrDefault(),
            };

            var jobsDto = await _sqlQueryExecutor.ExecuteQueryAsync<JobDto>(JobsQueryHelper.GetJobsPagedQuery(), parameters);

            int totalCount = 0;
            if (jobsDto.HasItems())
            {
                totalCount = jobsDto.FirstOrDefault().TotalCount;
            }

            return new PagedResponse<JobDto>(jobsDto, request.Page, request.PageSize, totalCount);
        }

        public async Task<SummaryJobsDto> GetSummaryJobs(GetJobsSummaryRequest request)
        {
            var parameters = new
            {
                CustomerId = request.CustomerId.GetValueOrDefault(),
            };

            var jobsDto = await _sqlQueryExecutor.ExecuteQueryAsync<SummaryJobsDto>(JobsQueryHelper.GetSummaryJobs(), parameters);

            return jobsDto.FirstOrDefault();

        }

        public async Task<ResponseBase> CompleteJOb(CompleteJobRequest request)
        {
            if(request == null)
            {
                return new ResponseBase { ValidationErrorMessage = Messages.NoInfoWasFoundToProcessRequest };
            }

            var job = await _repository.GetSingleAsync<Job>(s=>s.JobId == request.JobId);

            if (job.IsNull())
            {
                return new ResponseBase { ValidationErrorMessage = Messages.MustSelectAJob };
            }

            if (job.IsCompleted()) {
                return new ResponseBase { ValidationErrorMessage = Messages.ServiceRequestAlreadyBeenCompleted };
            }

            job.Complete();

            _repository.UnitOfWork.Commit(request.GetTransactionInfo(Transactions.CompleteJob));

            return new ResponseBase { SuccessMessage = Messages.RequestServiceWasCompleted };
        }

        public async Task<JobDto> GetJobById(GetJobByIdRequest request)
        {
            var parameters = new
            {
                JobId = request.JobId,
                CustomerId = request.CustomerId.GetValueOrDefault()
            };

            var jobsDto = await _sqlQueryExecutor.ExecuteQueryAsync<JobDto>(JobsQueryHelper.GetJobByIdQuery(), parameters);

            return jobsDto.FirstOrDefault();
        }

        public async Task<ResponseBase> UpdateJob(UpdateJobRequest request)
        {                        
            if (request.IsNull())
            {
                return new ResponseBase { ValidationErrorMessage = Messages.NoInfoWasFoundToProcessRequest };
            }

            var job = await _repository.GetSingleAsync<Job>(s => s.JobId == request.JobId);

            if (job.IsNull()) { 
                
                return new ResponseBase { ValidationErrorMessage = Messages.ServiceDoesNotExist };
            }

            if (job.IsCompleted())
            {
                return new ResponseBase { ValidationErrorMessage = Messages.RequestedServiceAlreadyCompleted };
            }

            if (request.SupplierId <= 0)
            {
                return new ResponseBase { ValidationErrorMessage = Messages.MustSelectSupplier };
            }

            var jobAssigned = await _repository.GetSingleAsync<JobAssignment>(s => s.JobId == request.JobId);

            if (jobAssigned.IsNull()) {
                jobAssigned = new JobAssignment
                {
                    AssignedAt = request.AssignedAt,
                };
                _repository.Add(jobAssigned);
            }

            jobAssigned.Notes = request.Notes;
            jobAssigned.JobId = request.JobId;
            jobAssigned.SupplierId = request.SupplierId;
            job.Status = request.SupplierId > 0 && !job.IsCompleted() ? Status.InProcess : Status.Pending;

            try
            {
                _repository.UnitOfWork.Commit(request.GetTransactionInfo(Transactions.UpdateJob));
            }
            catch (Exception ex)
            {

                var s = ex;
            }
            

            return new ResponseBase { SuccessMessage = Messages.SuccessfullySavedRecords };
        }
    }
}
