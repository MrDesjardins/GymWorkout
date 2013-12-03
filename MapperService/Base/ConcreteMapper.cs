using System;

namespace MapperService.Base
{
    public class ConcreteMapper
    {
        public ConcreteMapper(Type model, Type viewModel, IMapper profile)
        {
            Model = model;
            ViewModel = viewModel;
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