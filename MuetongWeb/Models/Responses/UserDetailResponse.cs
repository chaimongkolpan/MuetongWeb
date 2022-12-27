using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class UserDetailResponse
    {
        public string? Message { get; set; }
        public string? SuccessMessage { get; set; }
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? EmployeeId { get; set; }
        public string? CitizenId { get; set; }
        public UserDetailResponse() { }
        public UserDetailResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Address = user.Address;
            PhoneNo = user.PhoneNo;
            Email = user.Email;
            EmployeeId = user.EmployeeId;
            CitizenId = user.CitizenId;
        }
        public void SetMessage(string message) 
        {
            Message = message;
        }
        public void SetSuccess(string message)
        {
            SuccessMessage = message;
        }
    }
}
