using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class CustomerCollectionResponse
    {
        public int Page { get; set; } = 1;
        public List<int> Pages { get; set; } = new List<int>();
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; } = 0;
        public List<CustomerResponse> Customers { get; set; } = new List<CustomerResponse>();
        public CustomerCollectionResponse() { }
        public CustomerCollectionResponse(IEnumerable<Customer> customers, int count, int page = 1, int pageSize = 10)
        {
            TotalCount = count;
            Page = page;
            PageSize = pageSize;
            foreach (var customer in customers)
            {
                Customers.Add(new CustomerResponse(customer));
            }
            int sum = 0;
            int num = 1;
            while (count - sum > 0)
            {
                Pages.Add(num);
                num++;
                sum += pageSize;
            }
        }
    }
}
