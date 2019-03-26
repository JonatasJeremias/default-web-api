using Core.Api.Response;
using Core.Api.Validation;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Core.Api.BaseController
{
    public abstract partial class BaseController
    {
        /// <summary></summary>
        protected NegotiatedContentResult<ApiResponse<TResponse>> Handle<TRequest, TResponse>(
            TRequest args,
            Func<TRequest, TResponse> action)
        //where TRequest : class, new()
        {
            args?.ThrowExceptionIfInvalid();
            return HandleInternal(args, () =>
            {
                var ret = action(args);
                ret?.ThrowExceptionIfInvalid();
                return ret;
            });
        }


        /// <summary></summary>
        protected NegotiatedContentResult<ApiResponse<TResponse>> Handle<TResponse>(
            Func<TResponse> action)
        //where TRequest : class, new()
        {
            return HandleInternal(() =>
            {
                var ret = action();
                ret?.ThrowExceptionIfInvalid();
                return ret;
            });
        }


        /// <summary></summary>
        protected IHttpActionResult Handle<TRequest>(
            TRequest args,
            Action<TRequest> action)
        //where TRequest : class, new()
        {
            args?.ThrowExceptionIfInvalid();
            return HandleInternal<object>(() =>
            {
                action(args);
                return null;
            });
        }

        /// <summary></summary>
        protected IHttpActionResult Handle(Action action)
        {
            return HandleInternal<object>(() =>
            {
                action();
                return null;
            });
        }
    }
}
