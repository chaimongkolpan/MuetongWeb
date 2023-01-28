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
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string CitizenId { get; set; } = string.Empty;
        public long LineId { get; set; }
        public long DepartmentId { get; set; }
        public long SubDepartmentId { get; set; }
        public long ProvinceId { get; set; }
        public UserDetailResponse() { }
        public UserDetailResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            Firstname = string.IsNullOrWhiteSpace(user.Firstname) ? string.Empty : user.Firstname;
            Lastname = string.IsNullOrWhiteSpace(user.Lastname) ? string.Empty : user.Lastname;
            Address = string.IsNullOrWhiteSpace(user.Address) ? string.Empty : user.Address;
            PhoneNo = string.IsNullOrWhiteSpace(user.PhoneNo) ? string.Empty : user.PhoneNo;
            Email = string.IsNullOrWhiteSpace(user.Email) ? string.Empty : user.Email;
            EmployeeId = string.IsNullOrWhiteSpace(user.EmployeeId) ? string.Empty : user.EmployeeId;
            CitizenId = string.IsNullOrWhiteSpace(user.CitizenId) ? string.Empty : user.CitizenId;
            LineId = user.SubDepartment.Department.LineId;
            DepartmentId = user.SubDepartment.DepartmentId;
            SubDepartmentId = user.SubDepartmentId;
            ProvinceId = user.ProvinceId.HasValue ? user.ProvinceId.Value : 1;
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
