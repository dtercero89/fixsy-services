namespace FixsyWebApi.DTO.Users
{
    public class LoginDto : ResponseBase
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserRolesString { get; set; }
        public List<string>? Permissions { get; set; }
        public string CreationDate { get; set; }
        public bool IsCustomer { get; set; }
        public int CustomerId { get; set; }
    }
}
