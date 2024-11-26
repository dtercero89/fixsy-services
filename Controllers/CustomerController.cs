using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.Services.Customer;
using FixsyWebApi.DTO.Jobs;
using FixsyWebApi.DTO.Customer;
using Swashbuckle.AspNetCore.Annotations;
using FixsyWebApi.DTO;

namespace FixsyWebApi.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomersAppService _customersAppService;
        public CustomerController(CustomersAppService customersAppService)
        {
            _customersAppService = customersAppService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Publish a new job", Description = "Publishes a new job request submitted by the customers.")]
        [SwaggerResponse(200, "Job successfully published.", typeof(JobDto))]
        [SwaggerResponse(400, "Invalid request data.", typeof(JobDto))]
        public async Task<IActionResult> PublichJob(PublichJobRequest request)
        {
            var response = await _customersAppService.PublichJob(request);
           return Ok(response);
        }

        [HttpGet("jobs-paged")]
        [SwaggerOperation(Summary = "Get a paginated list of jobs", Description = "Retrieves a paginated list of jobs published by the user.")]
        [SwaggerResponse(200, "List of jobs", typeof(PagedResponse<JobDto>))]
        [SwaggerResponse(400, "Invalid query parameters.", typeof(GetCustomerJobsRequest))]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<IActionResult> GetPublishedJobs([FromQuery]GetCustomerJobsRequest request)
        {
            var response = await _customersAppService.GetPublishedJobsPaged(request);
            return Ok(response);
        }

    }
}