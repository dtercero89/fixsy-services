
using FixsyWebApi.Data.Extensions;

namespace FixsyWebApi.Data.Agg
{
    public class SupplierServices : EntityBase
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public string GetServiceName()
        {
            return Service.IsNotNull() ? Service.Name : string.Empty;
        }
    }
}
