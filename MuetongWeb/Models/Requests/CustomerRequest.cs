namespace MuetongWeb.Models.Requests
{
    public class CustomerRequest
    {
        public string? Query { get; set; }
        public long? ProvinceId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
