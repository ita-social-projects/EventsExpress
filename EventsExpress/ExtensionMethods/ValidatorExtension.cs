using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.Exceptions;
using FluentValidation;

namespace EventsExpress.ExtensionMethods
{
    public static class ValidatorExtension
    {
        public static void ValidateAndThrowIfInvalid<T>(this IValidator<T> validator, T model)
        {
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string> exept = new Dictionary<string, string>();
                var erors = validationResult.Errors.Select(e => new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage));
                foreach (var eror in erors)
                {
                    exept.Add(eror.Key, eror.Value);
                }

                throw new EventsExpressException("validation failed", exept);
            }
        }
    }
}
