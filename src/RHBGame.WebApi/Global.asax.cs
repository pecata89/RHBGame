using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using RHBGame.Data;

namespace RHBGame.WebApi
{
    public class Global : HttpApplication
    {
        void Application_Start(Object sender, EventArgs e)
        {
            // Force the creation of the databae (or update) at startup
            using (var db = new RHBGameRepository())
            {
                db.Topics.FirstOrDefault();
            }

            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                // Dependency injection
                var container = new UnityContainer();
                container.RegisterType<RHBGameRepository>();

                config.DependencyResolver = new DependencyResolver(container);
            });
        }


        private sealed class DependencyResolver : IDependencyResolver
        {
            private readonly IUnityContainer _container;


            public DependencyResolver(IUnityContainer container)
            {
                _container = container;
            }

            public Object GetService(Type serviceType)
            {
                try
                {
                    return _container.Resolve(serviceType);
                }
                catch (ResolutionFailedException)
                {
                    return null;
                }
            }


            public IEnumerable<Object> GetServices(Type serviceType)
            {
                try
                {
                    return _container.ResolveAll(serviceType);
                }
                catch (ResolutionFailedException)
                {
                    return Enumerable.Empty<Object>();
                }
            }

            public IDependencyScope BeginScope() => new DependencyResolver(_container.CreateChildContainer());


            public void Dispose() => _container.Dispose();
        }
    }
}