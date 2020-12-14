using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsExpress.ModelBinders
{
    public class TrimModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(CategoryCreateViewModel))
            {
                var request = bindingContext.HttpContext.Request;

                using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    var bodyString = reader.ReadToEnd();
                    var model = JsonSerializer.Deserialize<CategoryCreateViewModel>(bodyString);
                    model.Name = model.Name.Trim();

                    bindingContext.Result = ModelBindingResult.Success(model);
                }
            }

            if (bindingContext.ModelType == typeof(CategoryEditViewModel))
            {
                var request = bindingContext.HttpContext.Request;

                using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    var bodyString = reader.ReadToEnd();
                    var model = JsonSerializer.Deserialize<CategoryEditViewModel>(bodyString);
                    model.Name = model.Name.Trim();

                    bindingContext.Result = ModelBindingResult.Success(model);
                }
            }

            return Task.CompletedTask;
        }
    }
}
