using MuetongWeb.Helpers;
namespace MuetongWeb.Models.Pages
{
    public class PageModel
    {
        public bool IsAdmin { get; set; } = false;
        public bool IsAccount { get; set; } = false;
        public bool IsContractor { get; set; } = false;
        public bool IsPurchase { get; set; } = false;
        public bool IsManagement { get; set; } = false;
        public PageModel() { }
        public PageModel(string name)
        {
            Set(name);
        }
        public void Set(string name)
        {
            IsAccount = RoleHelpers.CanAccessAccountSection(name);
            IsAdmin = RoleHelpers.CanAccessAdminSection(name);
            IsContractor = RoleHelpers.CanAccessContractorSection(name);
            IsPurchase = RoleHelpers.CanAccessPurchaseSection(name);
            IsManagement = RoleHelpers.CanAccessManagerSection(name);
        }
    }
}
