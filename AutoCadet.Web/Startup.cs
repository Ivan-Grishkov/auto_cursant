using System.Web.Mvc;
using Antlr.Runtime.Misc;
using AutoCadet;
using AutoCadet.Infrastructure;
using Microsoft.Owin;
using Owin;
using StructureMap;

[assembly: OwinStartup(typeof(Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace AutoCadet
{
    public partial class Startup
    {
        private static IContainer _container;
        private StructureMapDependencyResolver _structureMapResolver;

        public void Configuration(IAppBuilder app)
        {
            _container = ContainerConfig.CreateContainer(app);
            _structureMapResolver = new StructureMapDependencyResolver(() => _container);
            DependencyResolver.SetResolver(_structureMapResolver);
            Func<IContainer> containerFactory =
                () => _structureMapResolver.CurrentNestedContainer ?? _container;

            ConfigureAuth(app, containerFactory);
        }
    }
}
