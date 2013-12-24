using BusinessLogic.Sessions;
using Mappers.Base;
using Model;

namespace Mappers.Definitions
{
    /// <summary>
    /// Allow to add additionnal method to the user sesssion and being injected.
    /// Mainly for testing purpose at this moment
    /// </summary>
    public interface IUserSessionDTOMapper : IMapper
    {
        UserSessionDTO GetDTO(ApplicationUser session);
        ApplicationUser GetModel(UserSessionDTO model);
    }
}