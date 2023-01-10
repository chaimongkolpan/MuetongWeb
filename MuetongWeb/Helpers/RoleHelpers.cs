using MuetongWeb.Constants;
namespace MuetongWeb.Helpers
{
    public static class RoleHelpers
    {
        public static bool CanAccessAdminSection(string role) 
        {
            return role == RoleConstants.RoleAdmin;
        }
        public static bool CanAccessAccountSection(string role)
        {
            return role == RoleConstants.RoleAdmin
                || role == RoleConstants.RoleManager
                || role == RoleConstants.RoleAccount;
        }
        public static bool CanAccessContractorSection(string role)
        {
            return role == RoleConstants.RoleAdmin
                || role == RoleConstants.RoleManager
                || role == RoleConstants.RoleContractor
                || role == RoleConstants.RoleContractorApprover;
        }
        public static bool CanAccessManagerSection(string role)
        {
            return role == RoleConstants.RoleAdmin
                || role == RoleConstants.RoleManager;
        }
        public static bool CanAccessPurchaseSection(string role)
        {
            return role == RoleConstants.RoleAdmin
                || role == RoleConstants.RoleManager
                || role == RoleConstants.RolePurchase
                || role == RoleConstants.RolePurchaseApprover;
        }
        public static bool CanSeeAllProject(string role)
        {
            return role == RoleConstants.RoleAdmin
                || role == RoleConstants.RoleManager;
        }
    }
}
