using MuetongWeb.Models.Pages;

namespace MuetongWeb.Models.Requests
{
    public class BillingApproveRequest
    {
        public UserInfoModel User { get; set; }
        public BillingApproveRequest() { }
        public BillingApproveRequest(UserInfoModel user)
        {
            User = user;
        }
    }
}
