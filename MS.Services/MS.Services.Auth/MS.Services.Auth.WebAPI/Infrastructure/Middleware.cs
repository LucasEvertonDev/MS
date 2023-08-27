using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;
using Serilog.Context;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace MS.Services.Auth.WebAPI.Infrastructure
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Read and log request body data
            string requestBodyPayload = await ReadRequestBody(context.Request);
           
            var httpRequestInfo = new
            {
                Host = context.Request.Host.ToString(),
                Path = context.Request.Path,
                Scheme = context.Request.Scheme,
                Method = context.Request.Method,
                Protocol = context.Request.Protocol,
                QueryString = context.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Headers = context.Request.Headers
                .Where(x => x.Key != "Cookie" && x.Key != "Authorization") // remove Cookie from header since it is analysed separatly
                .ToDictionary(x => x.Key, y => y.Value.ToString()),
                Cookies = context.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Body = requestBodyPayload
            };

            LogContext.PushProperty("Request", JsonSerializer.Serialize(httpRequestInfo));

            await _next(context);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);

            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            string requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;


            return requestBody;
        }
    }
}
