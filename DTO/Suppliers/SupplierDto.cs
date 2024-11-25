namespace FixsyWebApi.DTO.Suppliers
{
    public class SupplierDto : ResponseBase
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Services { get; set; }
        public string Status { get; set; }

        public int TotalCount { get; set; }
    }
}
