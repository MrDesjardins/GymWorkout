using System.Collections.Generic;
using AutoMapper;
using Mappers.Base;
using Model;
using ViewModels;

namespace Mappers.Implementations
{

    public class UserProfileMapper : ModelViewModelMapper<ApplicationUser, UserProfileViewModel>
    {


        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<ApplicationUser, UserProfileViewModel>()
                ;
            Mapper.CreateMap<UserProfileViewModel, ApplicationUser>()
            ;
        }
    }
     
}