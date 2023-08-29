using Microsoft.Extensions.DependencyInjection;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Services.UserServices;
using MS.Services.Auth.Test.Infrastructure;
using FluentAssertions;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.Contansts;

namespace MS.Services.Auth.Test.Services.UserServices;

public class CreateUserServiceTest : BaseTest
{
    private readonly ICreateUserService _createUserService;

    public CreateUserServiceTest()
    {
        _createUserService = _serviceProvider.GetService<ICreateUserService>();
    }

    [Fact]
    public async Task ValidateSucess()
    {
        await _createUserService.ExecuteAsync(new CreateUserModel()
        { 
            Email = "teste@teste.com" + new Random().Next(0, 10000),
            Password = "123456",
            Name = "Lucas Everton Santos de Oliveira",
            UserGroupId = "F97E565D-08AF-4281-BC11-C0206EAE06FA",
            Username = "lcseverton" + new Random().Next(0, 10000)
        });

        var retorno = _createUserService.CreatedUser;

        retorno.Id.Should().NotBeNull();
    }

    [Fact]
    public async Task ValidateUserAlreadyExists()
    {
        Func<Task> action = async () =>
        {
            await _createUserService.ExecuteAsync(new CreateUserModel()
            {
                Email = "lcseverton@gmail.com" + new Random().Next(0, 10000),
                Password = "123456",
                Name = "Lucas Everton Santos de Oliveira",
                UserGroupId = "F97E565D-08AF-4281-BC11-C0206EAE06FA",
                Username = "lcseverton"
            });
        };

        await action.Should().ThrowAsync<BusinessException>().Where(ex => ex.Error == UserErrors.Business.ALREADY_USERNAME);
    }
}
