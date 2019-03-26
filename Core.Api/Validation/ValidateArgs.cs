using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Api.Validation
{
    public class ValidateArgs
    {
        /// <summary></summary>
        public string Name { get; set; }

        /// <summary></summary>
        public object Object { get; set; }

        /// <summary></summary>
        public List<ValidationResult> Results { get; set; }
    }
}
