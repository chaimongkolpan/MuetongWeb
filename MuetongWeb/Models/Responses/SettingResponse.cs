namespace MuetongWeb.Models.Responses
{
    public class SettingResponse
    {
        public string? Message { get; set; }
        public SettingResponse() { }
        public SettingResponse(string? message)
        {
            Message = message;
        }
    }
}
