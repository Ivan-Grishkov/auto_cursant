using System;
using System.Web;
using System.Web.Mvc;
using AutoCadet.Infrastructure;
using Owin;
using StructureMap;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace AutoCadet
{
    public class ContainerConfig
    {
        private static StructureMapDependencyResolver _structureMapDependencyResolver;

        public static IContainer CreateContainer(IAppBuilder app)
        {
            app.Use(new Func<AppFunc, AppFunc>(next => (async env =>
            {
                // Pipeline hooks: http://benfoster.io/blog/quick-and-easy-owin-pipeline-hooks

                // Begin request
                _structureMapDependencyResolver.CreateNestedContainer();
                await next.Invoke(env);
                // End request
                _structureMapDependencyResolver.DisposeNestedContainer();
            })));

            var container = new Container();

            _structureMapDependencyResolver = new StructureMapDependencyResolver(() => container);
            Func<IContainer> containerFactory = () =>
            {
                // HttpContext.Current is null when SignalR send messages to clients. 
                return HttpContext.Current != null
                    ? _structureMapDependencyResolver.CurrentNestedContainer ?? container
                    : container;
            };

            container.Configure(cfg =>
            {
                cfg.AddRegistry(new AppRegistry());
            });

            DependencyResolver.SetResolver(_structureMapDependencyResolver);

            return container;
        }
    }
}