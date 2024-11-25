namespace FixsyWebApi.Data.Agg
{
    public class Supplier : EntityBase
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
    }
}
