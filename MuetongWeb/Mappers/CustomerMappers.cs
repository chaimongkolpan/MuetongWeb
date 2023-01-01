using MuetongWeb.Constants;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Mappers
{
    public static class CustomerMappers
    {
        public static List<Customer>? ImportCustomerMapper(List<ExcelCustomerData> request, long userId)
        {
            var result = new List<Customer>();
            foreach (ExcelCustomerData customer in request)
            {
                var temp = ExcelData(customer, userId);
                result.Add(temp);
            }
            return result;
        }
        public static Customer ExcelData(ExcelCustomerData request, long userId)
        {
            var customer = new Customer();
            customer.Name = request.Name;
            customer.Detail = request.Detail;
            customer.Address = request.Address;
            customer.ProvinceId = request.ProvinceId;
            customer.PhoneNo = request.PhoneNo;
            customer.Email = request.Email;
            customer.TaxNo = request.TaxNo;
            customer.BranchNo = request.BranchNo;
            customer.UserId = userId;
            customer.CreateDate = DateTime.Now;
            return customer;
        }
    }
}
