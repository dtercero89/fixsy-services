namespace FixsyWebApi.DTO
{
    public class PagedResponse<T>
    {
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<T> Items { get; set; }
                                               
        public PagedResponse(IEnumerable<T> items, int pageNumber, int pageSize, int totalItems)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize); 
        }
    }
}
