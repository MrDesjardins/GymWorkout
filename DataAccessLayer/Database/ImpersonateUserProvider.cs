using BusinessLogic;
using Model.Definitions;

namespace DataAccessLayer.Database
{
    public class ImpersonateUserProvider : ICurrentUser
    {
        public ImpersonateUserProvider()
        {
            
        }

        public ImpersonateUserProvider(ICurrentUser userProfile)
        {
            UserId = userProfile.UserId;
            UserName = userProfile.UserName;
            Email = userProfile.Email;
            Language = userProfile.Language;
        }


        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
    }
}