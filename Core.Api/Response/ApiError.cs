using System.Collections.Generic;

namespace Core.Api.Response
{
    public class ApiError
    {
        /// <summary></summary>
        public string Code { get; set; }

        /// <summary></summary>
        public string Message { get; set; }

        /// <summary></summary>
        public string MessageDetail { get; set; }

        /// <summary></summary>
        public Dictionary<string, string> AffectedData { get; set; }
    }
}
