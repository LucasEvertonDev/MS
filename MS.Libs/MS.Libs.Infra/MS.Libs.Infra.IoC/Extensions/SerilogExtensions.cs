using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MS.Libs.Core.Domain.Infra.AppSettings;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Configuration;
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

        Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().MinimumLevel.Override("Microsoft", LogEventLevel.Fatal).MinimumLevel.Override("System", LogEventLevel.Fatal).WriteTo
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
