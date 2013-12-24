using System.Web;
using System.Web.Providers.Entities;
using Microsoft.AspNet.Identity;
using Model;
using Model.Definitions;
using Services.Definitions;

namespace DataAccessLayer
{
    
    public class WebUserProvider : IUserProvider
    {
        private readonly IAccountService _service;
        public WebUserProvider(IAccountService service)
        {
            _service = service;
        }

        #region Implementation of IUserProvider

        public ICurrentUser Account
        {
            get
            {
                var currentUser = System.Web.HttpContext.Current.User.Identity;
                if (currentUser.IsAuthenticated)
                {
                   return _service.GetByUserName(currentUser.Name);
                }
                else
                {
                    return new ApplicationUser { Language = "fr-CA" };//Default user values    
                }
            }
        }

        #endregion
    }

    public class WebServiceUserProvider : IUserProvider
    {
        public ICurrentUser Account {
            get
            {
                return new ApplicationUser {UserName = HttpContext.Current.User.Identity.Name};
                
            }
        }
    }
    
}