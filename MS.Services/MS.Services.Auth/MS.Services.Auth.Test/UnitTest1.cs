using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Infra.Utils.Activator;
using MS.Services.Auth.Core.Domain.Services.AuthServices;
using MS.Services.Auth.Infra.Data.Contexts;
using MS.Services.Auth.Infra.IoC;
using System;

namespace MS.Services.Auth.Test
{
    public class UnitTest1
    {
        private readonly DbFixture _Fixture;

        public UnitTest1()
        {
            _Fixture = new DbFixture();
        }

        public class DbFixture
        {
            public DbFixture()
            {
                var serviceCollection = new ServiceCollection();

                App.Init<DependencyInjection>()
                    .AddInfraSctructure(serviceCollection);


                ServiceProvider = serviceCollection.BuildServiceProvider();
            }

            public ServiceProvider ServiceProvider { get; private set; }
        }

        [Fact]
        public async Task Test1()
        {
            var loginService = _Fixture.ServiceProvider.GetService<ILoginService>();

            await loginService.ExecuteAsync(new Core.Domain.Models.Auth.LoginModel { Username = "lcseverton", Password = "123456" });

            var retorno = loginService.TokenRetorno;
        }
    }
}