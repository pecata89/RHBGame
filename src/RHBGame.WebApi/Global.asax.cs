using System;
using System.Web;
using System.Web.Http;

namespace RHBGame.WebApi
{
    public class Global : HttpApplication
    {
        void Application_Start(Object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            });
        }
    }
}