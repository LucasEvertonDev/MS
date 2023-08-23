using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Infra.Utils.Activator;
using MS.Services.Auth.Infra.IoC;

namespace MS.Services.Auth.Test.Infrastructure;

public class BaseTest
{
    public ServiceProvider _serviceProvider { get; private set; }

    public BaseTest()
    {
        var serviceCollection = new ServiceCollection();

        App.Init<DependencyInjection>().AddInfraSctructure(serviceCollection,
            new AppSettings() { DbConnection = "Server=localhost,11433;Database=AuthDb;User Id=sa;Password=Numsey#2021;TrustServerCertificate=True;" });

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}
