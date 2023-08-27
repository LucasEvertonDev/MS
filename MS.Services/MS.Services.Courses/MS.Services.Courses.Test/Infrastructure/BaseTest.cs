using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Infra.Utils.Activator;
using MS.Services.Courses.Infra.IoC;

namespace MS.Services.Courses.Test.Infrastructure;

public class BaseTest
{
    public ServiceProvider _serviceProvider { get; private set; }

    public BaseTest()
    {
        var serviceCollection = new ServiceCollection();

        App.Init<DependencyInjection>().AddInfraSctructure(serviceCollection,
            new AppSettings() { SqlConnections = new SqlConnections ( "Server=FIVBR-APP002V\\QA;Database=AuthDb;User Id=Course_sign;Password=sign;TrustServerCertificate=True;", "") });

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}
