using FixsyWebApi.Data;
using FixsyWebApi.Data.Agg;
using FixsyWebApi.Data.Extensions;
using FixsyWebApi.Data.Helper;
using FixsyWebApi.Data.Repository;
using FixsyWebApi.DTO;
using FixsyWebApi.DTO.Customer;
using FixsyWebApi.DTO.Jobs;
using FixsyWebApi.Resources;
using FixsyWebApi.Services.Jobs;
using static Quartz.Logging.OperationName;

namespace FixsyWebApi.Services.Customer
{

    public class CustomersAppService
    {
        private readonly IGenericRepository<IFixsyDataContext> _repository;
        private readonly PostgreSqlQueryExecutor _sqlQueryExecutor;

        public CustomersAppService(IGenericRepository<IFixsyDataContext> repository,
            PostgreSqlQueryExecutor sqlQueryExecutor)
        {
            _repository = repository;
            _sqlQueryExecutor = sqlQueryExecutor;
        }

        public async Task<JobDto> PublichJob(PublichJobRequest request)
        {
            var validations = await CanPublishJob(request);

            if (validations.IsNotNull())
            {
                return new JobDto { ValidationErrorMessage = validations.ValidationErrorMessage };
            }

            var jobDto = request.Job;

            var existingJob = await _repository.GetSingleAsync<Data.Agg.Job>(j => j.JobId == jobDto.JobId);

            if(existingJob.IsNotNull() && existingJob.IsCompleted())
            {
                return new JobDto { ValidationErrorMessage = Messages.RequestedServiceAlreadyCompleted };
            }

            if (existingJob.IsNull())
            {

                existingJob = new Data.Agg.Job.Builder().WithCustomer(request.CustomerId.GetValueOrDefault())
                     .WithJob(jobDto.Title, jobDto.Description, jobDto.PreferredSchedule,
                                 jobDto.CreatedAt.GetValueOrDefault(), Status.Pending)
                     .WithService(jobDto.ServiceId, jobDto.Requirements)
                     .Build();

                _repository.Add(existingJob);
            }
            existingJob.Description = jobDto.Description;
            existingJob.PreferredSchedule = jobDto.PreferredSchedule;
            existingJob.Requirements = jobDto.Requirements;
            existingJob.ServiceId = jobDto.ServiceId;
            existingJob.Title = jobDto.Title;

            _repository.UnitOfWork.Commit(request.GetTransactionInfo(Transactions.PublishJOb));

            return new JobDto { SuccessMessage = Messages.SuccessfullyPublishedWork, JobId = existingJob.JobId };
        }


        private async Task<ResponseBase> CanPublishJob(PublichJobRequest request)
        {
            if (request == null || request.Job.IsNull()) {
                return new ResponseBase { ValidationErrorMessage = Messages.EnterJobDetail };
            }

            var dto = request.Job;
            if (request.CustomerId <= 0) {

                return new ResponseBase { ValidationErrorMessage = Messages.CustomerInfoNotFound };
            }

            if (dto.Description.IsMissingValue()) {

                return new ResponseBase { ValidationErrorMessage = Messages.EnterJobDescription };
            }

            if (dto.ServiceId <= 0) {
                return new ResponseBase { ValidationErrorMessage = Messages.DefineServiceRequired };
            }

            if (dto.Title.IsMissingValue()) {

                return new ResponseBase { ValidationErrorMessage = Messages.EnterJobTitle };
            }

            var existingCustomer = await _repository.GetSingleAsync<Data.Agg.Customer>(c=>c.CustomerId == request.CustomerId);
            if (existingCustomer.IsNull()){

                return new ResponseBase { ValidationErrorMessage = Messages.CustomerInfoNotFound };
            }

            var existingService = await _repository.GetSingleAsync<Data.Agg.Service>(c => c.ServiceId == dto.ServiceId);
            if (existingService.IsNull())
            {
                return new ResponseBase { ValidationErrorMessage = Messages.ServiceDoesNotExist };
            }

            return null;
        }


        public async Task<PagedResponse<JobDto>> GetPublishedJobsPaged(GetCustomerJobsRequest request)
        {
            var parameters = new
            {
                PageNumber = request.Page,
                PageSize = request.PageSize,
                SearchValue = request.SearchValue,
                Status = request.Status,
                CustomerId = request.CustomerId,
            };

            var jobsDto = await _sqlQueryExecutor.ExecuteQueryAsync<JobDto>(JobsQueryHelper.GetCustomerJobsPagedQuery(), parameters);

            int totalCount = 0;
            if (jobsDto.HasItems())
            {
                totalCount = jobsDto.FirstOrDefault().TotalCount;
            }

            return new PagedResponse<JobDto>(jobsDto, request.Page, request.PageSize, totalCount);

        }
    }
}