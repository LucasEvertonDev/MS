using FluentValidation;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Infra.Plugins.FluentValidation.CustomValidators;

namespace MS.Services.Auth.Infra.Plugins.FluentValidation.User;

public class CreateUserValidator : BaseValidator<UserModel>, IValidatorModel<UserModel>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Username).SetValidator(new UserNameValidator());
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email é obrigatorio");
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email inválido").WithErrorCode("Teste de erro");
        });
    }
}
