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

namespace MS.Services.Auth.Infra.IoC.Extensions;

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

        // Em "AdditionalColumns, adicione colunas extras que deseje, como IP, nome de usuário, id de trace da chamada, etc.
        var options = new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn> {
                        new SqlColumn { ColumnName = "Action", DataLength=  50, DataType = System.Data.SqlDbType.NVarChar }
                    }
        };

        Log.Logger = new LoggerConfiguration().WriteTo
           .MSSqlServer(
               connectionString: appSettings.SqlConnections.SerilogConnection,
               sinkOptions: new MSSqlServerSinkOptions
               {
                   TableName = "AppLogs",
                   AutoCreateSqlTable = true,
                   AutoCreateSqlDatabase = true,
                   LevelSwitch = levelSwitch
               },
               restrictedToMinimumLevel: (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Log.LogLevel),
               formatProvider: null,
               columnOptions: options,
               logEventFormatter: null
               )
           .CreateLogger();
    }
}
