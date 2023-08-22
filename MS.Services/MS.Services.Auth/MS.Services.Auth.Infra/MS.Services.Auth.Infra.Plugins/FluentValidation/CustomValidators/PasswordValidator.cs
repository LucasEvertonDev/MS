using FluentValidation;
using MS.Services.Auth.Core.Domain.Contansts;

namespace MS.Services.Auth.Infra.Plugins.FluentValidation.CustomValidators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(senha => senha).NotEmpty().WithMessage(UserErrors.PASSWORD_INVALID.ErrorMessage).WithErrorCode(UserErrors.PASSWORD_INVALID.ErrorCode);
        When(senha => !string.IsNullOrWhiteSpace(senha), () =>
        {
            RuleFor(senha => senha.Length).GreaterThanOrEqualTo(6).WithMessage(UserErrors.EMAIL_INVALID.ErrorMessage).WithErrorCode(UserErrors.EMAIL_INVALID.ErrorCode);
        });
    }
}
