using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.Services.Service;
using FixsyWebApi.DTO.Services;


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

        [HttpGet]
        public async Task<IActionResult> GetServices([FromQuery] GetServicesRequest request)
        {
            var response = await _servicesAppService.GetServices(request);
            return Ok(response);
        }

    }
}