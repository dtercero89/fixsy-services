namespace FixsyWebApi.DTO.Users
{

    public class RegisterUserRequest : RequestBase
    {
        public UserDto User { get; set; }
    }

    public class LoginRequest 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GetUserByIdRequest
    {
        public int Id { get; set; }
    }
}
