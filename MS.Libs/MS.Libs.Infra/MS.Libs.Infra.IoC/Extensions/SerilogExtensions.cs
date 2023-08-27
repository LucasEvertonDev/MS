using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Infra.Claims;
using MS.Libs.Infra.Utils.Extensions;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;
using Log = Serilog.Log;

namespace MS.Libs.Infra.IoC.Extensions;

public static class SerilogExtensions
{
    public static void RegisterSerilog(this HostBuilderContext app, IConfigurationBuilder configurationBuilder)
    {
        // app.HostingEnvironment.IsDevelopment()

        var settings = configurationBuilder.Build();

        var appSettings = new AppSettings(settings);

        var levelSwitch = new LoggingLevelSwitch()
        {
            MinimumLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Log.LogLevel),
        };

        var options = new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn { ColumnName = "ClientId", DataLength=  50, DataType = System.Data.SqlDbType.NVarChar },
                new SqlColumn { ColumnName = "UserId", DataLength=  50, DataType = System.Data.SqlDbType.NVarChar },
                new SqlColumn { ColumnName = "Request", DataLength=  -1, DataType = System.Data.SqlDbType.NVarChar },
            }
        };

        Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .Enrich.With<UserEnricher>()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
          .MinimumLevel.Override("System", LogEventLevel.Fatal)
          .WriteTo
             .MSSqlServer(
                 connectionString: appSettings.SqlConnections.SerilogConnection,
                 sinkOptions: new MSSqlServerSinkOptions
                 {
                     TableName = "AppLogs",
                     AutoCreateSqlTable = true,
                     AutoCreateSqlDatabase = true,
                     LevelSwitch = levelSwitch,
                 },
                 restrictedToMinimumLevel: (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Log.LogLevel),
                 formatProvider: null,
                 columnOptions: options,
                 logEventFormatter: null
                 )
         .CreateLogger();
    }
}

public class UserEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserEnricher() : this(new HttpContextAccessor())
    {
    }

    //Dependency injection can be used to retrieve any service required to get a user or any data.
    //Here, I easily get data from HTTPContext
    public UserEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var x = logEvent.Level;

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "UserId", _httpContextAccessor.HttpContext?.User?.Identity.GetUserClaim(JWTUserClaims.UserId) ?? "anonymous"));


        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ClientId", _httpContextAccessor.HttpContext?.User?.Identity.GetUserClaim(JWTUserClaims.ClientId) ?? "anonymous"));

    }

    private LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory, HttpContext httpContext)
    {
        var httpRequest = httpContext.Request;

        string body = "";

        Task.WaitAll(
            Task.Run(async () =>
            {
                if (httpRequest.ContentLength.HasValue && httpRequest.ContentLength > 0)
                {
                    string aux = await httpRequest.GetRawBodyAsync();

                    body = aux;
                }
            }));
        

        var httpRequestInfo = new HttpRequestInfo()
        {
            Host = httpRequest.Host.ToString(),
            Path = httpRequest.Path,
            Scheme = httpRequest.Scheme,
            Method = httpRequest.Method,
            Protocol = httpRequest.Protocol,
            QueryString = httpRequest.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Headers = httpRequest.Headers
                        .Where(x => x.Key != "Cookie" && x.Key != "Authorization") // remove Cookie from header since it is analysed separatly
                        .ToDictionary(x => x.Key, y => y.Value.ToString()),
            Cookies = httpRequest.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Body = body
        };

        var httpRequestProperty = propertyFactory.CreateProperty("Request", httpRequestInfo, true);

        return httpRequestProperty;
    }

    /// <summary>
    /// Helper class to run async methods within a sync process.
    /// </summary>
    internal static class AsyncUtil
    {
        private static readonly TaskFactory _taskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// Executes an async Task method which has a void return value synchronously
        /// USAGE: AsyncUtil.RunSync(() => AsyncMethod());
        /// </summary>
        /// <param name="task">Task method to execute</param>
        public static void RunSync(Func<Task> task)
            => _taskFactory
                .StartNew(task)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        /// <summary>
        /// Executes an async Task<T> method which has a T return type synchronously
        /// USAGE: T result = AsyncUtil.RunSync(() => AsyncMethod<T>());
        /// </summary>
        /// <typeparam name="TResult">Return Type</typeparam>
        /// <param name="task">Task<T> method to execute</param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> task)
            => _taskFactory
                .StartNew(task)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
    }
}

public static class Teste
{
    public static async Task<string> GetRawBodyAsync(
       this HttpRequest request,
       Encoding encoding = null)
    {
        if (!request.Body.CanSeek)
        {
            // We only do this if the stream isn't *already* seekable,
            // as EnableBuffering will create a new stream instance
            // each time it's called
            request.EnableBuffering();
        }

        var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);

        var body = await reader.ReadToEndAsync().ConfigureAwait(false);

        request.Body.Position = 0;

        return body;
    }

}

internal class HttpRequestInfo
{
    public HttpRequestInfo()
    {
    }

    public string Host { get; set; }
    public PathString Path { get; set; }
    public string Scheme { get; set; }
    public string Method { get; set; }
    public string Protocol { get; set; }
    public Dictionary<string, string> QueryString { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public Dictionary<string, string> Cookies { get; set; }
    public string Body { get; set; }
}
