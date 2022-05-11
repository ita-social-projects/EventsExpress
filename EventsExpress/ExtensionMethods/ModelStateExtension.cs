using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsExpress.ExtensionMethods
{
    public static class ModelStateExtension
    {
        public static void ThrowIfInvalid(this ModelStateDictionary modelState, string message)
        {
            if (!modelState.IsValid)
            {
                Dictionary<string, string> exept = new Dictionary<string, string>();

                foreach (var key in modelState.Keys)
                {
                    var error = modelState[key].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        exept.Add(key, error.ErrorMessage);
                    }
                }

                throw new EventsExpressException(message, exept);
            }
        }
    }
}
