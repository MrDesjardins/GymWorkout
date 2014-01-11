using System;

namespace Mappers.Base
{
    public interface IMapper
    {
        void Register();

        Type GetSourceType();
        Type GetDestinationType();
    }
}