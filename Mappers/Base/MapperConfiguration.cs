using AutoMapper;
using Mappers.Factory;

namespace Mappers.Base
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