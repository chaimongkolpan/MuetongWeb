using MuetongWeb.Models.Requests;

namespace MuetongWeb.Helpers
{
    public static class RequestHelpers
    {
        public static UserRequest GetUserRequest(HttpRequest request)
        {
            var userRequest = new UserRequest();
            try
            {
                userRequest.Query = request.Query["textsearch"];
                userRequest.Page = GetIntWithDefault(request.Query["page"], 1);
                userRequest.PageSize = GetIntWithDefault(request.Query["pagesize"], 10);
            }
            catch
            {
                userRequest = new UserRequest();
            }
            return userRequest;
        }
        public static CustomerRequest GetCustomerRequest(HttpRequest request)
        {
            var customerRequest = new CustomerRequest();
            try
            {
                customerRequest.Query = request.Query["textsearch"];
                customerRequest.Page = GetIntWithDefault(request.Query["page"], 1);
                customerRequest.PageSize = GetIntWithDefault(request.Query["pagesize"], 10);
            }
            catch
            {
                customerRequest = new CustomerRequest();
            }
            return customerRequest;
        }
        public static ContractorRequest GetContractorRequest(HttpRequest request)
        {
            var contractorRequest = new ContractorRequest();
            try
            {
                contractorRequest.Query = request.Query["textsearch"];
                contractorRequest.Page = GetIntWithDefault(request.Query["page"], 1);
                contractorRequest.PageSize = GetIntWithDefault(request.Query["pagesize"], 10);
            }
            catch
            {
                contractorRequest = new ContractorRequest();
            }
            return contractorRequest;
        }
        private static int GetIntWithDefault(string? query, int value)
        {
            if(string.IsNullOrEmpty(query))
                return value;
            int result = value;
            if(int.TryParse(query, out result))
                return result;
            return result;
        }
    }
}
