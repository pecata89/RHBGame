using System;
using System.Collections.Generic;
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
            _expiredSessionsTimer = new Timer(_ => ClearExpiredSessions(), null, TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(20));
        }

        private void ClearExpiredSessions()
        {
            // TODO: Implement expired sessions removal
            using (var db = new RHBGameRepository())
            {
                foreach (var session in db.Sessions)
                {
                    if (DateTime.UtcNow - session.Created > _timeOut)
                    {
                        db.Sessions.Remove(db.Sessions.Find(session.Token));
                        db.SaveChanges();
                    }
                    else if (DateTime.UtcNow - session.LastActivity > _timeOut)
                    {
                        // Entity state delete
                        db.Sessions.Remove(db.Sessions.Find(session.Token));
                        // SQL Delete executed
                        db.SaveChanges();
                    }
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