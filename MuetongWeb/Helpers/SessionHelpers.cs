using Newtonsoft.Json;
using MuetongWeb.Models.Pages;
using MuetongWeb.Constants;

namespace MuetongWeb.Helpers
{
    public static class SessionHelpers
    {
        public static bool SessionAlive(ISession session)
        {
            if (session == null || !session.IsAvailable)
                return false;
            var sessionData = session.GetString(SessionConstants.UserInfoKey);
            if (string.IsNullOrWhiteSpace(sessionData))
                return false;
            return true;
        }
        public static UserInfoModel? GetUserInfo(ISession session)
        {
            var sessionData = session.GetString(SessionConstants.UserInfoKey);
            if (string.IsNullOrWhiteSpace(sessionData))
                return null;
            return JsonConvert.DeserializeObject<UserInfoModel>(sessionData);
        }
        public static void SetUserInfo(ISession session, UserInfoModel userInfo)
        {
            var userInfoData = JsonConvert.SerializeObject(userInfo);
            session.SetString(SessionConstants.UserInfoKey, userInfoData);
        }
        public static UserInfoModel? GetUserInfo(string? session)
        {
            if (string.IsNullOrWhiteSpace(session))
                return null;
            return JsonConvert.DeserializeObject<UserInfoModel>(session);
        }
        public static string SetUserInfo(UserInfoModel userInfo)
        {
            return JsonConvert.SerializeObject(userInfo);
        }
    }
}
