using Microsoft.AspNetCore.Mvc;
using FixsyWebApi.DTO.Users;
using FixsyWebApi.Services.Users;

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

        [HttpPost()]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var response = await _usersAppservice.RegisterUser(request);
            return Ok(response);
        }

        [HttpPost("log-in")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var response = await _usersAppservice.LoginUser(request);
            return Ok(response);
        }

        [HttpPost("id")]
        public async Task<IActionResult> GetUserById(GetUserByIdRequest request)
        {
            var response = await _usersAppservice.GetUserById(request);
            return Ok(response);
        }

    }
}