namespace FixsyWebApi.DTO.Suppliers
{
    public class GetSuppliersRequest
    {
        public string SearchValue { get; set; }
    }

    public class GetSuppliersByIdRequest
    {
        public int SupplierId { get; set; }
    }

    public class GetSuppliersPagedRequest : PagedRequestBase
    {
        
    }

    public class CreateSupplierRequest : RequestBase
    {
        public SupplierDto Supplier { get; set; }
    }
}
