using System.Globalization;

namespace MS.Infra.WebApi.Infrastructure.Middlewares;
public class AuthClientMiddleware
{
    private readonly RequestDelegate _next;

    public AuthClientMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var cultura = new CultureInfo("pt");

        if (context.Request.Headers.ContainsKey("Accept-Language"))
        {
            var linguagem = context.Request.Headers["Accept-Language"].ToString();
        }

        await _next(context);
    }
}
