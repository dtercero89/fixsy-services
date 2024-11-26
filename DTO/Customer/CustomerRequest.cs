namespace FixsyWebApi.DTO.Customer
{
    public class CustomerRequest : RequestBase
    {
        public string Name { get; set; }
    }

    
    public class CustomerRideRequest : RequestBase
    {
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public int Rate { get; set; }
        public int CentralId { get; set; }
    }



    public class  GetCustomerJobsRequest : PagedRequestBase
    {
        public string Status { get; set; }
    }
}
