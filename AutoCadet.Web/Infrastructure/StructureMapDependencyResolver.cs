using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;

namespace AutoCadet.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private const string NestedContainerKey = "Nested.Container.Key";
        private readonly Func<IContainer> _containerFactory;

        public StructureMapDependencyResolver(Func<IContainer> containerFactory)
        {
            _containerFactory = containerFactory;
        }

        public IContainer CurrentNestedContainer
        {
            get { return (IContainer)HttpContext.Items[NestedContainerKey]; }
            set { HttpContext.Items[NestedContainerKey] = value; }
        }

        private HttpContextBase HttpContext
        {
            get
            {
                var ctx = _containerFactory().TryGetInstance<HttpContextBase>();
                return ctx ?? new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            var container = GetContainer();

            return serviceType.IsAbstract || serviceType.IsInterface
                ? container.TryGetInstance(serviceType)
                : container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var container = CurrentNestedContainer ?? _containerFactory();
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        public void DisposeNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                CurrentNestedContainer.Dispose();
                CurrentNestedContainer = null;
            }
        }

        public void CreateNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                return;
            }
            CurrentNestedContainer = _containerFactory().GetNestedContainer();
        }

        public IContainer GetContainer()
        {
            return CurrentNestedContainer ?? _containerFactory();
        }
    }
}