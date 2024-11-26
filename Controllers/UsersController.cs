using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.DTO.Users;
using FixsyWebApi.Services.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace FixsyWebApi.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        
        private readonly UsersAppService _usersAppservice;
        public UsersController(UsersAppService usersAppservice)
        {
            _usersAppservice = usersAppservice;
        }
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request">The user registration data including necessary details.</param>
        /// <returns>A response indicating the success or failure of the user registration.</returns>
        [HttpPost()]
        [SwaggerOperation(Summary = "Register a new user", Description = "Registers a new user in the system with the provided registration data.")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var response = await _usersAppservice.RegisterUser(request);
            return Ok(response);
        }

        /// <summary>
        /// Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="request">The login data including the username and password.</param>
        /// <returns>A response containing authentication tokens if login is successful.</returns>
        [HttpPost("log-in")]
        [SwaggerOperation(Summary = "Login a user", Description = "Logs in a user by verifying their credentials and returns authentication tokens.")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var response = await _usersAppservice.LoginUser(request);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a user's information by their unique identifier.
        /// </summary>
        /// <param name="request">The request containing the user's ID to fetch their details.</param>
        /// <returns>The user's details for the given ID, if found.</returns>
        [HttpPost("id")]
        [SwaggerOperation(Summary = "Get user by ID", Description = "Retrieves the user's details based on their unique identifier.")]
        public async Task<IActionResult> GetUserById(GetUserByIdRequest request)
        {
            var response = await _usersAppservice.GetUserById(request);
            return Ok(response);
        }

    }
}