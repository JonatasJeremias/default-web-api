using Core.Api.Response;
using Core.Api.Validation;
using System;
using System.Linq;
using System.Net;
using System.Web.Http.Results;

namespace Core.Api.BaseController
{
    public abstract partial class BaseController
    {
        private NegotiatedContentResult<ApiResponse<TResponse>> HandleInternal<TResponse>(
            Func<TResponse> handler)
        {
            try
            {
                var start = DateTime.UtcNow;

                // Calculate and log the result.
                var result = new ApiResponse<TResponse>(handler());

                // Return the result.
                var ret = LoggedContent(HttpStatusCode.OK, result);

                return ret;
            }
            catch (Exception ex)
            {
                return HandleException<TResponse>(ex);
            }
        }

        private NegotiatedContentResult<ApiResponse<TResponse>> HandleInternal<TRequest, TResponse>(
            TRequest args,
            Func<TResponse> handler)
        {
            try
            {
                // Validate the input and return a bad request.
                var validation = args.Validate();
                if (validation.Errors.Any())
                {
                    var ret = new ApiResponse<TResponse>();
                    ret.Errors.Add(new ApiError { Message = validation.ToDescErrorsString() });
                    return Content(HttpStatusCode.BadRequest, ret);
                }

                // Calculate and log the result.
                var result = new ApiResponse<TResponse>(handler());

                // Return the result.
                return LoggedContent(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return HandleException<TResponse>(ex);
            }
        }
    }
}
