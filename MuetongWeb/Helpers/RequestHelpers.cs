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
