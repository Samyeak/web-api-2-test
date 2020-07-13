using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;

namespace WebAPI.Core
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;
        public UnityResolver(IUnityContainer _container)
        {
            if (_container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = _container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}