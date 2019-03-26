using Core.Api.Config;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Core.Api.Extensions
{
    public static class HttpApplicationExtensions
    {

        /// <summary></summary>
        public static void ExtendStart(this HttpApplication httpApplication)
        {
            // Swagger UI
            SwaggerConfig.Register();

            // WebApi
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // http://stackoverflow.com/questions/19467673/entity-engine-self-referencing-loop-detected
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;


        }
    }
}
