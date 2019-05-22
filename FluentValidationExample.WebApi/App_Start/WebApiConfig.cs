using System.Web.Http;
using FluentValidation.WebApi;
using FluentValidationExample.WebApi.Filters;

namespace FluentValidationExample.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            //Fluent Validation
            config.Filters.Add(new ValidateModelStateFilter());
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
