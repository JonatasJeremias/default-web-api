using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace Core.Api.BaseController
{
    public abstract partial class BaseController : ApiController
    {
        private NegotiatedContentResult<T> LoggedContent<T>(HttpStatusCode statusCode, T result)
        {
            return Content(statusCode, result);
        }
    }
}
