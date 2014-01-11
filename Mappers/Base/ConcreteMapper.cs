using System;
using AutoMapper;

namespace Mappers.Base
{
    /// <summary>
    /// Specify for a Model and ViewModel which Auto ClassMapper profile to use
    /// </summary>
    public class ConcreteMapper
    {
        public ConcreteMapper(Type model, Type viewModel, IMapper profile)
        {
            Model = model;
            ViewModel = viewModel;
            Profile = profile;
        }

        public ConcreteMapper(IMapper profile)
        {
            Model = profile.GetSourceType();
            ViewModel = profile.GetDestinationType();
            Profile = profile;
        }

        public Type Model { get; private set; }
        public Type ViewModel { get; private set; }
        public IMapper Profile { get; private set; }

        public void Register()
        {
            Profile.Register();
        }
    }
}