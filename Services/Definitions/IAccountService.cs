using Model;
using Services.Base;
using ViewModels;

namespace Services.Definitions
{
    public interface IAccountService : IService<UserProfile>
    {
        void CreateUserProfile(UserProfile userProfile);
        UserProfile GetByUserName(string toLower);

    }
}