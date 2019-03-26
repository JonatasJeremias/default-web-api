using System;
using System.Net;
using System.Web.Http.Results;
using Core.Api.Response;

namespace Core.Api.BaseController
{
    public abstract partial class BaseController
    {
        private NegotiatedContentResult<ApiResponse<TResponse>> HandleException<TResponse>(Exception ex)
        {
            var ret = new ApiResponse<TResponse>();

            var myEx = ex;
            do
            {
                ret.Errors.Add(new ApiError
                {
                    Message = myEx.Message,
                    MessageDetail = myEx.StackTrace
                });

                myEx = myEx.InnerException;
            }
            while (myEx != null);

            // 500 errors.
            return LoggedContent(HttpStatusCode.InternalServerError, ret);
        }
    }
}
