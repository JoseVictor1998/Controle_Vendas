using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using ComunicacaoVisual.API.Contracts; // <- IMPORTANTE
using System.Collections.Generic;
using System.Linq;

namespace ComunicacaoVisual.API.Models;

public class SwaggerFileUploadFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasUploadDto = context.MethodInfo.GetParameters()
            .Any(p => p.ParameterType == typeof(UploadArteRequest));

        if (!hasUploadDto) return;

        operation.RequestBody = new OpenApiRequestBody
        {
            Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Required = new HashSet<string> { "itemId", "file" },
                        Properties =
                        {
                            ["itemId"] = new OpenApiSchema { Type = "integer", Format = "int32" },
                            ["file"]   = new OpenApiSchema { Type = "string", Format = "binary" }
                        }
                    }
                }
            }
        };
    }
}