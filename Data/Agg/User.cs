using FixsyWebApi.Data.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixsyWebApi.Data.Agg
{
    public class User : EntityBase
    {
        public User()
        {

        }
        public User(string name, string email, string password, string phoneNumber, byte[] img)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Photo = img;
            Password = password;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Password { get; private set; }
        public byte[]? Photo { get; private set; }

        public virtual ICollection<Customer> Customers { get; private set; }

        internal void AddCustomer(Customer newCustomer)
        {
            if (Customers == null) { 
                Customers = new List<Customer>();
            }
            Customers.Add(newCustomer);
        }

        internal int GetCustomerId()
        {
            if (Customers.HasItems())
            {
                return Customers.FirstOrDefault().CustomerId;
            }
            return 0;
        }
    }
}
