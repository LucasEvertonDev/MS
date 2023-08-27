using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MS.Services.Gateway.WebAPI.Infrastructure.SwaggerFilters;

public class RemoveAuthotizationHeader : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var scopes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();

        if (scopes.Any())
        {
            operation.Parameters = operation.Parameters.Where(param => param.Name?.ToLower() != "authorization").ToList();
        }
    }
}