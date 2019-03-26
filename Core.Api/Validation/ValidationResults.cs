using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Api.Validation
{
    public class ValidationResults
    {
        // https://www.codeproject.com/Articles/1184173/DataAnnotations-in-Depth

        /// <summary>Method responsible for the throw.</summary>
        public MethodBase Method { get; set; }

        /// <summary>Container of the calling method, e.g. the class.</summary>
        public Type Class { get; set; }

        /// <summary>Type of the variable that caused the error.</summary>
        public Type Argument { get; set; }

        /// <summary></summary>
        public IEnumerable<ValidateArgs> Errors { get; set; }


        /// <summary>Validates the object and throws an ValidationException if any error is found.</summary>
        //[DebuggerStepThrough]
        public void ThrowExceptionIfHasErrors()
        {
            if (!Errors.Any()) return;
            var msg = this.ToDescErrorsString();
            var ex = new Exception(msg);
            throw ex;
        }

    }


    /// <summary></summary>
    public static class ValidationResultsExtensions
    {

        /// <summary></summary>
        //[DebuggerStepThrough]
        public static string ToDescErrorsString(this ValidationResults source)
        {
            var result = new StringBuilder();

            if (!source.Errors.Any())
                return result.ToString();

            result.AppendLine("Validations errors:");
            result.AppendLine($"- {source.Argument?.Name} @ {source.Class?.Name}.{source.Method?.Name}");

            source.Errors.ToList().ForEach(e =>
            {
                if (!e.Results.Any()) return;
                result.AppendLine($"#{e.Name} ({e.Object.GetType().Name})");

                foreach (var res in e.Results)
                {
                    result.AppendLine(
                        $"  .{res.MemberNames.FirstOrDefault()} " +
                        $"--> {res.ErrorMessage}");
                }
            });

            return result.ToString();
        }
    }
}
