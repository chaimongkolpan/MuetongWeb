using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public UserResponse() { }
        public UserResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Fullname = string.Format("{0} {1}", user.Firstname, user.Lastname);
        }
    }
}
