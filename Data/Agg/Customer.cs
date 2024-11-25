using System.ComponentModel.DataAnnotations;

namespace FixsyWebApi.Data.Agg
{
    public class Customer : EntityBase
    {
        public Customer(string address)
        {
           Address = address;
        }

        public Customer()
        {

        }

        [Key]
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Address { get; private set; }

        public virtual User User { get; set; }

    }
}
