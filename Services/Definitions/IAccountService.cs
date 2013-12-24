using Microsoft.AspNet.Identity;
using Model;
using Services.Base;
using ViewModels;

namespace Services.Definitions
{

    public interface IAccountService : IService<ApplicationUser>
    {
        void CreateUserProfile(ApplicationUser userProfile);
        ApplicationUser GetByUserName(string toLower);
        ApplicationUser GetById(string id);

        UserManager<ApplicationUser> GetUserManager();
    }
     
}