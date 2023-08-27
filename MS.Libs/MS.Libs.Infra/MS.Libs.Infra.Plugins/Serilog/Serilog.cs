using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using MS.Libs.Core.Domain.Infra.Claims;
using MS.Libs.Infra.Utils.Extensions;
using System.Text.Json;

namespace MS.Libs.Infra.Plugins.Serilog;

public class Serilog : ISerilog
{
    private readonly string _clientId;
    private readonly string _userId;
    private readonly string _request;
    private readonly IServiceProvider _services;

    public Serilog(IServiceProvider services, IHttpContextAccessor httpContextAccessor)
    {
        var identiy = httpContextAccessor.HttpContext?.User?.Identity;
        var request = httpContextAccessor.HttpContext?.Request;
        if (identiy != null && !string.IsNullOrEmpty(identiy.GetUserClaim(JWTUserClaims.UserId)))
        {
            _clientId = identiy.GetUserClaim(JWTUserClaims.ClientId);
            _userId = identiy.GetUserClaim(JWTUserClaims.UserId);
            _request = JsonSerializer.Serialize(new { header = request?.Headers, url = request?.GetDisplayUrl(), body = request?.Body });
        }
        ////_logger = logger;
    }

    public void LogInformation<T>(string message)
    {
        ApplyScope<T>((log) =>
        {
            log.LogInformation(message);
        });
    }

    public void LogWarning<T>(string message)
    {
        ApplyScope<T>((log) =>
        {
            log.LogWarning(message);
        });
    }

    public void LogError<T>(string message)
    {
        ApplyScope<T>((log) =>
        {
            log.LogError(message);
        });
    }

    public void LogError<T>(Exception ex, string message)
    {
        ApplyScope<T>((log) =>
        {
            log.LogError(ex, message);
        });
    }

    private void ApplyScope<T>(Action<ILogger<T>> log)
    {
        var _logger = _services.GetService<ILogger<T>>();

        using (_logger.BeginScope("{ClientId}", _clientId))
        {
            using (_logger.BeginScope("{UserId}", _userId))
            {
                using (_logger.BeginScope("{Request}", _request))
                {
                    log(_logger);
                }
            }
        }
    }
}
