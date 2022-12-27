namespace MuetongWeb.Models.Pages
{
    public class LoginModel
    {
        public string? Message { get; set; }
        public LoginModel() { }
        public LoginModel(string message) 
        { 
            Message = message;
        }
    }
}
