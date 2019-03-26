using System.Collections.Generic;

namespace Core.Api.Response
{
    public class ApiResponse
    {
        private Dictionary<string, string> _headers;
        /// <summary></summary>
        public Dictionary<string, string> Headers
        {
            get { return _headers ?? (_headers = new Dictionary<string, string>()); }
            set { _headers = value; }
        }


        private List<ApiError> _errors;
        /// <summary></summary>
        public List<ApiError> Errors
        {
            get { return _errors ?? (_errors = new List<ApiError>()); }
            set { _errors = value; }
        }


        private List<ApiError> _warnings;
        /// <summary></summary>
        public List<ApiError> Warnings
        {
            get { return _warnings ?? (_warnings = new List<ApiError>()); }
            set { _warnings = value; }
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        /// <summary></summary>
        public T Result { get; set; }

        /// <summary></summary>
        public ApiResponse(T result)
        {
            Result = result;
        }

        /// <summary></summary>
        public ApiResponse()
        {
        }
    }
}
