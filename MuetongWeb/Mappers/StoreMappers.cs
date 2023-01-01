using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public class StoreMappers
    {
        public static List<Store>? ImportCustomerMapper(List<ExcelStoreData> request, long userId)
        {
            var result = new List<Store>();
            foreach (ExcelStoreData store in request)
            {
                var temp = ExcelData(store, userId);
                result.Add(temp);
            }
            return result;
        }
        public static Store ExcelData(ExcelStoreData request, long userId)
        {
            var store = new Store();
            store.Name = request.Name;
            store.Address = request.Address;
            store.ProvinceId = request.ProvinceId;
            store.PhoneNo = request.PhoneNo;
            store.TaxNo = request.TaxNo;
            store.ContractName = request.ContractName;
            store.Email = request.Email;
            store.UserId = userId;
            store.CreateDate = DateTime.Now;
            return store;
        }
    }
}
