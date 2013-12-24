using System.Web;
using BusinessLogic.Sessions;
using Microsoft.AspNet.Identity;

namespace Setup.Sessions
{
    public class HttpSessionHandler : ISessionHandler
    {


        #region Implementation of ISessionHandler

        public void SaveUser(UserSessionDTO dto)
        {
            HttpContext.Current.Session["user"] = dto;
        }

        public UserSessionDTO GetUser()
        {
            var s = HttpContext.Current.User.Identity.GetUserName();
            return (UserSessionDTO)HttpContext.Current.Session["user"];
        }

        public void RemoveUser()
        {
            HttpContext.Current.Session.Remove("user");
        }

        #endregion
    }
}