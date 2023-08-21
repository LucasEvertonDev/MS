using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MS.Libs.WebApi.Infrastructure.Filters;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MS.Libs.WebApi.Infrastructure.Extensions;

public static class SwaggerExtensions
{
    public static void RegisterSwaggerDefaultConfig(this SwaggerGenOptions options, bool useJwt)
    {
        options.ExampleFilters();

        if (useJwt)
        {
            // add Security information to each operation for OAuth2
            options.OperationFilter<SecurityRequirementsOperationFilter>();

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
        }

        options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

        options.TagActionsBy(api =>
        {
            if (api.GroupName != null)
            {
                return new[] { api.GroupName };
            }

            var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                return new[] { controllerActionDescriptor.ControllerName };
            }

            throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });

        options.DocInclusionPredicate((name, api) => true);

        options.OperationFilter<SwaggerDefaultValues>();
    }
}
