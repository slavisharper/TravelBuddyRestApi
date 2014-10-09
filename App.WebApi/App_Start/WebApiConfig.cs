namespace App.WebApi
{
    using Microsoft.Owin.Security.OAuth;
    using System.Web.Http;
    using System.Web.Http.Cors;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Location",
                routeTemplate: "api/user/location",
                defaults: new { controller = "location" }
            );

            config.Routes.MapHttpRoute(
                name: "Favourites",
                routeTemplate: "api/user/favourites",
                defaults: new { controller = "favourites" }
            );

            config.Routes.MapHttpRoute(
                name: "PlaceEdit",
                routeTemplate: "api/travels/{id}/places",
                defaults: new { controller = "travelplaces" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
