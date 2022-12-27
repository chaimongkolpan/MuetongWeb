namespace MuetongWeb.Models.Requests
{
    public class UserChangePasswordRequest
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
