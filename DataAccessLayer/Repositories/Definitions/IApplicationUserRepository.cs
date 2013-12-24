using DataAccessLayer.Repositories.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;

namespace DataAccessLayer.Repositories.Definitions
{

    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        UserStore<ApplicationUser> GetUserStore();
    }
    
}