using FluentValidation;
using FluentValidation.Results;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Infra.Plugins.FluentValidation.CustomValidators;

internal class UserNameValidator : AbstractValidator<string>
{
    public UserNameValidator()
    {
        RuleFor(UserName => UserName).NotEmpty().WithMessage(UserErrors.USERNAME_REQUIRED.ErrorMessage).WithErrorCode(UserErrors.USERNAME_REQUIRED.ErrorCode);
        When(UserName => !string.IsNullOrWhiteSpace(UserName), () =>
        {
            RuleFor(UserName => UserName).Custom((username, contexto) =>
            {
                if (username.Contains(" "))
                {
                    contexto.AddFailure(new ValidationFailure(nameof(CreatedUserModel.Username), UserErrors.USERNAME_INVALID.ErrorMessage) { ErrorCode = UserErrors.USERNAME_INVALID.ErrorCode });
                }

                if (username.Length > 20)
                {
                    contexto.AddFailure(new ValidationFailure(nameof(CreatedUserModel.Username), UserErrors.USERNAME_INVALID.ErrorMessage) { ErrorCode = UserErrors.USERNAME_INVALID.ErrorCode });
                }
            });
        });
    }
}
