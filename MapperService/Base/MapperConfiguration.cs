using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapperService.Factory;

namespace MapperService.Base
{
    /// <summary>
    /// Add from a factory all mapper to AutoMapper
    /// </summary>
    public static class MapperConfiguration
    {
        public static void Initialize(IMapperFactory factory)
        {
            foreach (var mapperProfile in factory.MapperProfiles)
            {
                mapperProfile.Register();
            }
        }
    }
}
