using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.Services.Customer;
using FixsyWebApi.DTO.Jobs;
using FixsyWebApi.Services.Jobs;
using FixsyWebApi.Data.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.Annotations;

namespace FixsyWebApi.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly JobsAppService _jobsAppService;
        public JobsController(JobsAppService jobsAppService)
        {
            _jobsAppService = jobsAppService;
        }

        /// <summary>
        /// Retrieves a paginated list of jobs.
        /// </summary>
        /// <param name="request">The request parameters for pagination.</param>
        /// <returns>A paginated list of jobs.</returns>
        [HttpGet("paged")]
        [SwaggerOperation(Summary = "Get a paginated list of jobs", Description = "Retrieves a paginated list of jobs based on the provided request parameters.")]
        public async Task<IActionResult> GetJobsPaged([FromQuery]GetJobsRequest request)
        {
            var response = await _jobsAppService.GetJobsPaged(request);
            return Ok(response);
        }

        /// <summary>
        /// Assigns a job to a supplier.
        /// </summary>
        /// <param name="request">The request containing job details and the supplier to assign.</param>
        /// <returns>The result of the job assignment operation.</returns>
        [HttpPost("assign-job")]
        [SwaggerOperation(Summary = "Assign a job to a supplier", Description = "Assigns a job to a supplier based on the provided request details.")]
        public async Task<IActionResult> AssignJobToSupplier(AssignJobRequest request)
        {
            var response = await _jobsAppService.AssignJobToSupplier(request);
           return Ok(response);
        }

        /// <summary>
        /// Retrieves a summary of jobs.
        /// </summary>
        /// <param name="request">The request parameters to generate a summary of jobs.</param>
        /// <returns>A summary of jobs based on the provided filters.</returns>
        [HttpGet("summary")]
        [SwaggerOperation(Summary = "Get a summary of jobs", Description = "Retrieves a summary of jobs based on the given parameters.")]
        public async Task<IActionResult> GetSummaryJobs([FromQuery] GetJobsSummaryRequest request)
        {
            var response = await _jobsAppService.GetSummaryJobs(request);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a job by its unique identifier.
        /// </summary>
        /// <param name="request">The request containing the job's unique identifier.</param>
        /// <returns>The job details if found, or a NotFound result if the job does not exist.</returns>
        [HttpGet("by-id")]
        [SwaggerOperation(Summary = "Get a job by its ID", Description = "Retrieves the job details based on the job's unique identifier.")]
        public async Task<IActionResult> GetJobById([FromQuery] GetJobByIdRequest request )
        {
            var response = await _jobsAppService.GetJobById(request);
            return response.IsNull() ? NotFound() : Ok(response);
        }


        /// <summary>
        /// Marks a job as completed.
        /// </summary>
        /// <param name="request">The request containing the job details to be marked as complete.</param>
        /// <returns>The result of the completion operation.</returns>
        [HttpPut("complete")]
        [SwaggerOperation(Summary = "Complete a job", Description = "Marks a job as completed based on the provided request details.")]
        public async Task<IActionResult> CompleteJOb(CompleteJobRequest request)
        {
            var response = await _jobsAppService.CompleteJOb(request);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing job's details.
        /// </summary>
        /// <param name="request">The request containing the updated job details.</param>
        /// <returns>The result of the job update operation.</returns>
        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update job details", Description = "Updates the details of an existing job based on the provided request.")]
        public async Task<IActionResult> updateJob(UpdateJobRequest request)
        {
            var response = await _jobsAppService.UpdateJob(request);
            return Ok(response);
        }
    }
}