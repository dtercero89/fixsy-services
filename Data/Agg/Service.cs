using System.ComponentModel.DataAnnotations;

namespace FixsyWebApi.Data.Agg
{
    public class Service : EntityBase
    {
        [Key]
        public int ServiceId { get; set; }
        public string Name { get; set; }
        

        public virtual ICollection<SupplierServices> SupplierServices { get; set; } 

    }
}
