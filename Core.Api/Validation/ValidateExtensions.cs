using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace Core.Api.Validation
{
    /// <summary></summary>
    public static class ValidateExtensions
    {


        /// <summary>Validates the Data Annotations of the object recursively</summary>
        //[DebuggerStepThrough]
        public static void ThrowExceptionIfInvalid(this object obj, string name = null)
        {
            MyThrowExceptionIfInvalid(obj, name);
        }

        /// <summary>Validates the Data Annotations of the object recursively</summary>
        //[DebuggerStepThrough]
        public static void ThrowExceptionIfInvalid<T>(this T obj, string name = null)
        {
            MyThrowExceptionIfInvalid(obj, name);
        }

        //[DebuggerStepThrough]
        private static void MyThrowExceptionIfInvalid<T>(this T obj, string name = null)
        {
            // Get calling method name
            var frame = new StackTrace().GetFrame(2);
            var method = frame?.GetMethod();
            var @class = method?.DeclaringType;

            if (obj == null)
            {
                throw new ArgumentException(
                    $"Argument [{name ?? typeof(T).Name}] " +
                    $"in [{@class?.Name}.{method?.Name}] " +
                    "should not be null.");
            }

            // Validate the data annotations.
            var results = new List<ValidateArgs>();
            Validate(obj, results, obj.GetType().Name);

            var errors = new ValidationResults
            {
                Errors = results.Where(x => x.Results.Any()),
                Method = method,
                Class = @class,
                Argument = typeof(T),
            };
            errors.ThrowExceptionIfHasErrors();
        }



        /// <summary>Validates the Data Annotations of the object recursively</summary>
        [DebuggerStepThrough]
        public static ValidationResults Validate(this object obj)
        {
            var results = new List<ValidateArgs>();
            if (obj != null)
                Validate(obj, results, obj.GetType().Name);
            return new ValidationResults
            {
                Errors = results.Where(x => x.Results.Any())
            };
        }


        [DebuggerStepThrough]
        private static void Validate(object obj, ICollection<ValidateArgs> results, string name)
        {
            var valContext = new ValidationContext(obj, null, null);
            var validationsResults = new List<ValidationResult>();
            Validator.TryValidateObject(obj, valContext, validationsResults, true);

            results.Add(new ValidateArgs
            {
                Name = name,
                Object = obj,
                Results = new List<ValidationResult>(validationsResults)
            });

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.CustomAttributes.All(x =>
                    x.AttributeType != typeof(ValidateObjectAttribute)))
                    continue;
                var val = prop.GetValue(obj);
                if (val != null) Validate(val, results, prop.Name);
            }
        }

    }
}
