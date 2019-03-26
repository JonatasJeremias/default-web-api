using System.Web.Http;
using Core.Api.BaseController;
using Default.Service.Handlers;
using Swashbuckle.Swagger.Annotations;

namespace Default.WebApi.Controllers
{
    [RoutePrefix("api/notificatios")]
    public class DefaultController : BaseController
    {
        [HttpGet]
        [Route("ValidateApi")]
        [SwaggerOperation("ValidateApi")]
        public virtual IHttpActionResult ValidateApi()
        {
            var handler = new DefaultHandler();
            return Handle(handler.ValidateApi);
        }
    }
}
