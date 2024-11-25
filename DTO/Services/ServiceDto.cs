namespace FixsyWebApi.DTO.Services
{
    public class ServiceDto:ResponseBase
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
