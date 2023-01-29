using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class StoreCollectionResponse
    {
        public List<StoreResponse> Stores { get; set; } = new List<StoreResponse>();
        public StoreCollectionResponse() { }
        public StoreCollectionResponse(IEnumerable<Store> stores)
        {
            foreach (Store store in stores)
            {
                Stores.Add(new StoreResponse(store));
            }
        }
    }
}
