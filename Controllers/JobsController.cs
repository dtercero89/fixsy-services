using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.Services.Customer;
using FixsyWebApi.DTO.Jobs;
using FixsyWebApi.Services.Jobs;
using FixsyWebApi.Data.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

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

        [HttpGet("paged")]
        public async Task<IActionResult> GetJobsPaged([FromQuery]GetJobsRequest request)
        {
            var response = await _jobsAppService.GetJobsPaged(request);
            return Ok(response);
        }

        [HttpPost("assign-job")]
        public async Task<IActionResult> AssignJobToSupplier(AssignJobRequest request)
        {
            var response = await _jobsAppService.AssignJobToSupplier(request);
           return Ok(response);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummaryJobs([FromQuery] GetJobsSummaryRequest request)
        {
            var response = await _jobsAppService.GetSummaryJobs(request);
            return Ok(response);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetJobById([FromQuery] GetJobByIdRequest request )
        {
            var response = await _jobsAppService.GetJobById(request);
            return response.IsNull() ? NotFound() : Ok(response);
        }


        [HttpPut("complete")]
        public async Task<IActionResult> CompleteJOb(CompleteJobRequest request)
        {
            var response = await _jobsAppService.CompleteJOb(request);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> updateJob(UpdateJobRequest request)
        {
            var response = await _jobsAppService.UpdateJob(request);
            return Ok(response);
        }
    }
}