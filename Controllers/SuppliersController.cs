using FixsyWebApi.DTO.Suppliers;
using FixsyWebApi.Services.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace FixsyWebApi.Controllers
{

    [ApiController]
    [Route("api/v1/suppliers")]
    public class SuppliersController : ControllerBase
    {

        private readonly SuppliersAppServices _suppliersAppServices;

        public SuppliersController(SuppliersAppServices suppliersAppServices)
        {
            _suppliersAppServices = suppliersAppServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers([FromQuery] GetSuppliersRequest request)
        {
            var response = await _suppliersAppServices.GetSuppliers(request);
            return Ok(response);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetSuppliersById([FromQuery] GetSuppliersByIdRequest request)
        {
            var response = await _suppliersAppServices.GetSuppliersById(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(CreateSupplierRequest request)
        {
            var response = await _suppliersAppServices.CreateSupplier(request);
            return Ok(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetSuppliersPaged([FromQuery] GetSuppliersPagedRequest request)
        {
            var response = await _suppliersAppServices.GetSuppliersPaged(request);
            return Ok(response);
        }
    }
}