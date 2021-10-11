namespace EventsExpress.SwaggerSettings
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection.Metadata;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    [ExcludeFromCodeCoverage]
    public class ApplyDocumentExtension : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc != null)
            {
                foreach (var path in swaggerDoc.Paths)
                {
                    if (path.Key.StartsWith("/api/Category/Create") || path.Key.StartsWith("/api/Category/Edit"))
                    {
                        var referenceForSchema = path.Value.Operations[OperationType.Post].Parameters[0].Schema.Reference;
                        var myOpenApiMediaType = new OpenApiMediaType { Schema = new OpenApiSchema { Reference = referenceForSchema } };
                        var reqBodyContent = new Dictionary<string, OpenApiMediaType>();
                        reqBodyContent.Add("application/json", myOpenApiMediaType);
                        reqBodyContent.Add("text/json", myOpenApiMediaType);
                        reqBodyContent.Add("application/*+json", myOpenApiMediaType);
                        var myOpenReqBody = new OpenApiRequestBody { Content = reqBodyContent, Description = "Param defines CategoryView model" };
                        path.Value.Operations[OperationType.Post].RequestBody = myOpenReqBody;
                        path.Value.Operations[OperationType.Post].Parameters = null;
                    }
                }
            }
        }
    }
}
