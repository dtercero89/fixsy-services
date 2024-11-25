using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.Services.Customer;
using FixsyWebApi.DTO.Jobs;
using FixsyWebApi.DTO.Customer;

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
        public async Task<IActionResult> PublichJob(PublichJobRequest request)
        {
            var response = await _customersAppService.PublichJob(request);
           return Ok(response);
        }

        [HttpGet("jobs-paged")]
        public async Task<IActionResult> GetPublishedJobs([FromQuery]GetCustomerJobsRequest request)
        {
            var response = await _customersAppService.GetPublishedJobsPaged(request);
            return Ok(response);
        }

    }
}