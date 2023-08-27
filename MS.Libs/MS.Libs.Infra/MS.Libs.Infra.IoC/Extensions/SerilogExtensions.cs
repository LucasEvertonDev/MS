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
using Log = Serilog.Log;

namespace MS.Libs.Infra.IoC.Extensions;

public static class SerilogExtensions
{
    public static void RegisterSerilog(this HostBuilderContext app, IConfigurationBuilder configurationBuilder)
    {
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
            },
            Message = new ColumnOptions.MessageColumnOptions
            {
                DataLength = 1000
            },
            MessageTemplate = new ColumnOptions.MessageTemplateColumnOptions
            {
                DataLength = 1000
            },
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
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "UserId", _httpContextAccessor.HttpContext?.User?.Identity.GetUserClaim(JWTUserClaims.UserId) ?? "anonymous"));


        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ClientId", _httpContextAccessor.HttpContext?.User?.Identity.GetUserClaim(JWTUserClaims.ClientId) ?? "anonymous"));
    }
}

