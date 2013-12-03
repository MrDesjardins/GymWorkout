using System.Collections.Generic;
using AutoMapper;
using Mappers.Base;
using Model;
using ViewModels;

namespace Mappers.Implementations
{
    public class UserProfileMapper: ModelViewModelMapper<UserProfile, UserProfileViewModel>
    {


        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<UserProfile, UserProfileViewModel>()
                ;
            Mapper.CreateMap<UserProfileViewModel, UserProfile>()
            ;
        }
    }
}