using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class BillingIndexPoResponse
    {
        public List<PoResponse> Pos { get; set; } = new List<PoResponse>();
        public BillingIndexPoResponse() { }
        public BillingIndexPoResponse(IEnumerable<Po> pos)
        {
            foreach (var po in pos)
            {
                var tmp = new PoResponse(po);
                Pos.Add(tmp);
            }
        }
    }
}
