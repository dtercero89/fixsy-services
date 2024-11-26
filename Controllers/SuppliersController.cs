using FixsyWebApi.DTO.Suppliers;
using FixsyWebApi.Services.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Retrieves a list of suppliers based on the provided request parameters.
        /// </summary>
        /// <param name="request">The request parameters used to filter the suppliers.</param>
        /// <returns>A list of suppliers matching the provided filters.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of suppliers", Description = "Retrieves a list of suppliers based on the provided request parameters, such as filters and pagination.")]
        public async Task<IActionResult> GetSuppliers([FromQuery] GetSuppliersRequest request)
        {
            var response = await _suppliersAppServices.GetSuppliers(request);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a supplier by its unique identifier.
        /// </summary>
        /// <param name="request">The request parameters containing the supplier's ID.</param>
        /// <returns>The supplier details for the given ID.</returns>
        [HttpGet("by-id")]
        [SwaggerOperation(Summary = "Get supplier by ID", Description = "Retrieves the supplier details for the given unique identifier.")]
        public async Task<IActionResult> GetSuppliersById([FromQuery] GetSuppliersByIdRequest request)
        {
            var response = await _suppliersAppServices.GetSuppliersById(request);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new supplier in the system.
        /// </summary>
        /// <param name="request">The request data to create the supplier.</param>
        /// <returns>The details of the created supplier.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new supplier", Description = "Creates a new supplier in the system with the provided details.")]
        public async Task<IActionResult> CreateSupplier(CreateSupplierRequest request)
        {
            var response = await _suppliersAppServices.CreateSupplier(request);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a paginated list of suppliers based on the provided request parameters.
        /// </summary>
        /// <param name="request">The request parameters used to filter and paginate the suppliers.</param>
        /// <returns>A paginated list of suppliers matching the provided filters.</returns>
        [HttpGet("paged")]
        [SwaggerOperation(Summary = "Get paginated list of suppliers", Description = "Retrieves a paginated list of suppliers based on the provided request parameters.")]
        public async Task<IActionResult> GetSuppliersPaged([FromQuery] GetSuppliersPagedRequest request)
        {
            var response = await _suppliersAppServices.GetSuppliersPaged(request);
            return Ok(response);
        }
    }
}