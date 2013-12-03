namespace MapperService.Base
{
    public class ClassMapper : AutoMapper.Profile, IMapper
    {
        public void Register()
        {
            this.Configure();
        }
    }
}
