using AutoMapper;
using FixsyWebApi.DTO;

namespace FixsyWebApi.Data.Factories
{
    public class MapperFactory
    {
        private static readonly IMapper _mapper;

        static MapperFactory()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); 
            });
            _mapper = config.CreateMapper();
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}
