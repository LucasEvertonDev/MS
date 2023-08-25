using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Models.Error;
using System.Text.Json;

namespace MS.Libs.WebApi.Infrastructure.Middlewares;

public class AuthUnauthorizedMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
    private IEnumerable<int> _vallowedStatusCodes = new int[] { StatusCodes.Status401Unauthorized, StatusCodes.Status403Forbidden };

    public AuthUnauthorizedMiddleware(RequestDelegate next,
        AppSettings appSettings
    )
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);
        await WriteUnauthorizedResponseAsync(httpContext);
    }

    public async Task WriteUnauthorizedResponseAsync(HttpContext httpContext)
    {
        if (_vallowedStatusCodes.Contains(httpContext.Response.StatusCode) is false)
            return;

        var statusCode = httpContext.Response.StatusCode;
        var errormodel = new ErrorsModel();

        switch (statusCode)
        {
            case StatusCodes.Status401Unauthorized:
                errormodel = new ErrorsModel(_appSettings.Messages.Unauthorized);
                break;
            case StatusCodes.Status403Forbidden:
                errormodel = new ErrorsModel(_appSettings.Messages.Forbidden);
                break;
        }

        await httpContext.Response
            .WriteAsync(JsonSerializer.Serialize(errormodel));
    }
}
