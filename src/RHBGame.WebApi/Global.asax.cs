using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using RHBGame.Data;

namespace RHBGame.WebApi
{
    public class Global : HttpApplication
    {
        private Timer _expiredSessionsTimer;
        private TimeSpan _timeOut = TimeSpan.FromMinutes(20);

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

                // Dependency injection
                var container = new UnityContainer();
                container.RegisterType<RHBGameRepository>();
                container.RegisterType<AuthenticationHelper>();

                config.DependencyResolver = new DependencyResolver(container);
            });

            // Timer that will remove the expired user sessions
            _expiredSessionsTimer = new Timer(_ => ClearExpiredSessions(), null, TimeSpan.Zero, TimeSpan.FromMinutes(20));
        }

        private void ClearExpiredSessions()
        {
            using (var db = new RHBGameRepository())
            {
                var currentTime = DateTime.UtcNow;
                var expiredSessions = db.Sessions.Where(x => DbFunctions.DiffSeconds( x.LastActivity, currentTime) > _timeOut.TotalSeconds).ToList();

                if (expiredSessions.Count > 0)
                {
                    foreach (var session in expiredSessions)
                    {
                        db.Sessions.Remove(session);
                    }

                    db.SaveChanges();
                }
            }
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