using System.Web;
using Model;
using Model.Definitions;
using WebMatrix.WebData;

namespace BusinessLogic
{
    public class WebUserProvider : IUserProvider
    {

        #region Implementation of IUserProvider

        public Model.Definitions.ICurrentUser Account
        {
            get {

                if (WebSecurity.Initialized)
                {
                    var userProfile = new  UserProfile {UserId = WebSecurity.CurrentUserId, UserName = WebSecurity.CurrentUserName};
                    return userProfile;
                }
                 return new UserProfile { Language = "fr-CA"};//Default user values
                
            }
        }

        #endregion
    }

    public class WebServiceUserProvider : IUserProvider
    {
        public ICurrentUser Account {
            get {
                var name = HttpContext.Current.User.Identity.Name;
                return new UserProfile{UserName = name};
            }
        }
    }
}