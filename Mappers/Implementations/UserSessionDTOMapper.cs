using AutoMapper;
using BusinessLogic.Sessions;
using Mappers.Base;
using Mappers.Definitions;
using Model;

namespace Mappers.Implementations
{

    public class UserSessionDTOMapper : ClassMapper, IUserSessionDTOMapper
    {
        public UserSessionDTO GetDTO(UserProfile session)
        {
            return Mapper.Map<UserProfile, UserSessionDTO>(session);
        }

        public UserProfile GetModel(UserSessionDTO model)
        {
            return Mapper.Map<UserSessionDTO, UserProfile>(model);
        }
       
        protected override void Configure()
        {
            Mapper.CreateMap<UserProfile, UserSessionDTO>()
                .ForMember(d => d.UserId, option => option.MapFrom(e => e.UserId))
                .ForMember(d => d.Language, option => option.MapFrom(e => e.Language))
                ;
            Mapper.CreateMap<UserSessionDTO, UserProfile>()
                .ForMember(d => d.UserId, option => option.MapFrom(e => e.UserId))
                .ForMember(d => d.Language, option => option.MapFrom(e => e.Language))
                ;
        }

        
    }

   
}