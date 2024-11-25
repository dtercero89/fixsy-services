namespace FixsyWebApi.DTO
{
    public class PagedRequestBase : RequestBase
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SearchValue { get; set; }
    }
}
