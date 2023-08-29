using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Test.Infrastructure;

namespace MS.Services.Auth.Test.Validators.UserValidators;

public class CreateUserValidatorTest : BaseTest
{
    private readonly IValidatorModel<CreateUserModel> _createUserValidator;

    public CreateUserValidatorTest()
    {
        _createUserValidator = _serviceProvider.GetService<IValidatorModel<CreateUserModel>>();
    }

    [Fact]
    public async Task ValidateUsernameRequired()
    {
        Func<Task> action = async () =>
        {
            await _createUserValidator.ValidateModelAsync(new CreateUserModel()
            {
                Email = "lcseverton@gmail.com" + new Random().Next(0, 10000),
                Password = "123456",
                Name = "Lucas Everton Santos de Oliveira",
                UserGroupId = "F97E565D-08AF-4281-BC11-C0206EAE06FA",
            });
        };

        await action.Should().ThrowAsync<ValidatorException>().Where(ex => ex.ErrorsMessages != null 
            && ex.ErrorsMessages.Exists(a => a.ErrorCode == UserErrors.Validators.USERNAME_REQUIRED.Context));
    }

    [Fact]
    public async Task ValidateEmailInvalid()
    {
        Func<Task> action = async () =>
        {
            await _createUserValidator.ValidateModelAsync(new CreateUserModel()
            { 
                Username = "lcseverton",
                Email = "teste",
                Password = "123456",
                Name = "Lucas Everton Santos de Oliveira",
                UserGroupId = "F97E565D-08AF-4281-BC11-C0206EAE06FA",
            });
        };

        await action.Should().ThrowAsync<ValidatorException>().Where(ex => ex.ErrorsMessages != null
            && ex.ErrorsMessages.Exists(a => a.ErrorCode == UserErrors.Validators.EMAIL_INVALID.Context));
    }
}
