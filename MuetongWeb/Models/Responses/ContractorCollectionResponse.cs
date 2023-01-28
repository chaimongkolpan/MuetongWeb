using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class ContractorCollectionResponse
    {
        public int Page { get; set; } = 1;
        public List<int> Pages { get; set; } = new List<int>();
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; } = 0;
        public List<ContractorResponse> Contractors { get; set; } = new List<ContractorResponse>();
        public ContractorCollectionResponse() { }
        public ContractorCollectionResponse(IEnumerable<Contractor> contractors, int count, int page = 1, int pageSize = 10)
        {
            TotalCount = count;
            Page = page;
            PageSize = pageSize;
            foreach (var contractor in contractors)
            {
                Contractors.Add(new ContractorResponse(contractor));
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
