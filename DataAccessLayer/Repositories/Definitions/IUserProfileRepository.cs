using DataAccessLayer.Repositories.Base;
using Model;

namespace DataAccessLayer.Repositories.Definitions
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile GetByUserName(string toLower);
    }
}