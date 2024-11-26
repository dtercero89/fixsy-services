using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.Services.Service;
using FixsyWebApi.DTO.Services;
using Swashbuckle.AspNetCore.Annotations;


namespace FixsyWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/services")]
    public class ServicesController : ControllerBase
    {
        private readonly ServicesAppService _servicesAppService;
        
        public ServicesController(ServicesAppService servicesAppService)
        {
            _servicesAppService = servicesAppService;
        }

        /// <summary>
        /// Retrieves a list of services based on the provided request parameters.
        /// </summary>
        /// <param name="request">The request parameters used to filter the services.</param>
        /// <returns>A list of services matching the provided filters.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of services", Description = "Retrieves a list of services based on the provided request parameters, such as filters and pagination.")]
        public async Task<IActionResult> GetServices([FromQuery] GetServicesRequest request)
        {
            var response = await _servicesAppService.GetServices(request);
            return Ok(response);
        }

    }
}