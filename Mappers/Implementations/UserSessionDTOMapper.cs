using AutoMapper;
using BusinessLogic.Sessions;
using Mappers.Base;
using Mappers.Definitions;
using Model;

namespace Mappers.Implementations
{
    
    public class UserSessionDTOMapper : ClassMapper, IUserSessionDTOMapper
    {
        public UserSessionDTO GetDTO(ApplicationUser session)
        {
            return Mapper.Map<ApplicationUser, UserSessionDTO>(session);
        }

        public ApplicationUser GetModel(UserSessionDTO model)
        {
            return Mapper.Map<UserSessionDTO, ApplicationUser>(model);
        }
       
        protected override void Configure()
        {
            Mapper.CreateMap<ApplicationUser, UserSessionDTO>()
                .ForMember(d => d.UserId, option => option.MapFrom(e => e.UserId))
                .ForMember(d => d.Language, option => option.MapFrom(e => e.Language))
                ;
            Mapper.CreateMap<UserSessionDTO, ApplicationUser>()
                .ForMember(d => d.UserId, option => option.MapFrom(e => e.UserId))
                .ForMember(d => d.Language, option => option.MapFrom(e => e.Language))
                ;
        }

        
    }
    
   
}