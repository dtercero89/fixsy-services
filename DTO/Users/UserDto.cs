using FixsyWebApi.Data.Extensions;

namespace FixsyWebApi.DTO.Users
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        private string _email;
        public string Email
        {
            get { return _email.IsMissingValue() ? string.Empty : _email.Trim(); }
            set { _email = value; }
        }

        public string PhoneNumber { get; set; }
        //public byte[] Photo { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Address { get; set; }
        public bool IsCustomer { get; set; }
    }
}
