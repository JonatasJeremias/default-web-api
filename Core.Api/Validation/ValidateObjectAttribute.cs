using System.ComponentModel.DataAnnotations;

namespace Core.Api.Validation
{
    public class ValidateObjectAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
