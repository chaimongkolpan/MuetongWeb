using MuetongWeb.Helpers;
namespace MuetongWeb.Models.Pages
{
    public class PageModel
    {
        public UserInfoModel? UserInfo { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsAccount { get; set; } = false;
        public bool IsContractor { get; set; } = false;
        public bool IsPurchase { get; set; } = false;
        public bool IsManagement { get; set; } = false;
        public PageModel() { }
        public PageModel(UserInfoModel user)
        {
            Set(user);
        }
        public void Set(UserInfoModel user)
        {
            UserInfo = user;
            IsAccount = RoleHelpers.CanAccessAccountSection(user.Role);
            IsAdmin = RoleHelpers.CanAccessAdminSection(user.Role);
            IsContractor = RoleHelpers.CanAccessContractorSection(user.Role);
            IsPurchase = RoleHelpers.CanAccessPurchaseSection(user.Role);
            IsManagement = RoleHelpers.CanAccessManagerSection(user.Role);
        }
    }
}
