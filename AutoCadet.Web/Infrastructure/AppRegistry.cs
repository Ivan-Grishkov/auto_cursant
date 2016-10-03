using AutoCadet.Models.Mapper;
using AutoMapper;
using StructureMap;

namespace AutoCadet.Infrastructure
{
    public class AppRegistry:Registry
    {
        public AppRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory();
                scan.IncludeNamespace("AutoCadet");
                scan.WithDefaultConventions();
            });

            //AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoCadetMapperProfile>();
            });
            For<IMapper>().Use(context => config.CreateMapper()).Singleton();
        }
    }
}