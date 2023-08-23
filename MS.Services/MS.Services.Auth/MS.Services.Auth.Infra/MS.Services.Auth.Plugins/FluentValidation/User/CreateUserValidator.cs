using FluentValidation;
using FluentValidation.Results;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Plugins.Validators;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Plugins.FluentValidation.User;

public class CreateUserValidator : BaseValidator<CreateUserModel>, IValidatorModel<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Username).NotNull().WithMessage(UserErrors.USERNAME_REQUIRED.ErrorMessage).WithErrorCode(UserErrors.USERNAME_REQUIRED.ErrorCode);
        RuleFor(c => c.Email).NotEmpty().WithMessage(UserErrors.EMAIL_INVALID.ErrorMessage).WithErrorCode(UserErrors.EMAIL_INVALID.ErrorCode);
        RuleFor(c => c.Password).NotEmpty().WithMessage(UserErrors.PASSWORD_INVALID.ErrorMessage).WithErrorCode(UserErrors.PASSWORD_INVALID.ErrorCode);

        When(c => !string.IsNullOrWhiteSpace(c.Password), () =>
        {
            RuleFor(c => c.Password.Length).GreaterThanOrEqualTo(6).WithMessage(UserErrors.PASSWORD_INVALID.ErrorMessage).WithErrorCode(UserErrors.PASSWORD_INVALID.ErrorCode);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(UserErrors.EMAIL_INVALID.ErrorMessage).WithErrorCode(UserErrors.EMAIL_INVALID.ErrorCode);
        });
    }
}
