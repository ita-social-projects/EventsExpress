using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace EventsExpress.ModelBinders
{
    public class TrimModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;

            using var reader = new StreamReader(request.Body, Encoding.UTF8);
            var bodyString = reader.ReadToEnd();
            var ob = JsonSerializer.Deserialize(bodyString, bindingContext.ModelType);

            foreach (var prop in bindingContext.ModelType.GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    var propValue = prop.GetValue(ob) as string;
                    prop.SetValue(ob, propValue.Trim());
                }
            }

            bindingContext.Result = ModelBindingResult.Success(ob);
            await Task.CompletedTask;
        }
    }
}
