using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class UserCollectionResponse
    {
        public string? Message { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; } = 0;
        public List<UserResponse> Users { get; set; } = new List<UserResponse>();
        public UserCollectionResponse() { }
        public UserCollectionResponse(IEnumerable<User> users, int count, int page = 1, int pageSize = 10)
        {
            TotalCount = count;
            Page = page;
            PageSize = pageSize;
            foreach (var user in users)
            {
                Users.Add(new UserResponse(user));
            }
        }
        public UserCollectionResponse(string message)
        {
            Message = message;
        }
    }
}
