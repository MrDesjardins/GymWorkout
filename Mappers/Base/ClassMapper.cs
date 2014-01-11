using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.Base
{
    public abstract class ClassMapper : AutoMapper.Profile, IMapper
    {
        public void Register()
        {
            this.Configure();
        }

        public abstract Type GetSourceType();
        public abstract Type GetDestinationType();

    }
}
