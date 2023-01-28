using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class UserAddRequest
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Idcard { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long ProvinceId { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public long SubDepartmentId { get; set; }
        public UserInfoModel? User { get; set; }
    }
}
