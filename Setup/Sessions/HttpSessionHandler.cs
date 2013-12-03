using System.Web;
using BusinessLogic.Sessions;

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
            return (UserSessionDTO)HttpContext.Current.Session["user"];
        }

        public void RemoveUser()
        {
            HttpContext.Current.Session.Remove("user");
        }

        #endregion
    }
}