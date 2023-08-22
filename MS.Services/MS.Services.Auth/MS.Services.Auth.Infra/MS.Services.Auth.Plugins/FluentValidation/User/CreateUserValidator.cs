using FluentValidation;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Plugins.Validators;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Plugins.FluentValidation.CustomValidators;

namespace MS.Services.Auth.Plugins.FluentValidation.User;

public class CreateUserValidator : BaseValidator<CreateUserModel>, IValidatorModel<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Username).SetValidator(new UserNameValidator());
        RuleFor(c => c.Email).NotEmpty().WithMessage(UserErrors.EMAIL_INVALID.ErrorMessage).WithErrorCode(UserErrors.EMAIL_INVALID.ErrorCode);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(UserErrors.EMAIL_INVALID.ErrorMessage).WithErrorCode(UserErrors.EMAIL_INVALID.ErrorCode);
        });
    }
}
