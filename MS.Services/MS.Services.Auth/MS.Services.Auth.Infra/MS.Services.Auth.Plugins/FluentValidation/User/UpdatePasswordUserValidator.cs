using FluentValidation;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Plugins.Validators;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Plugins.FluentValidation.Extensions;

namespace MS.Services.Auth.Plugins.FluentValidation.User;

public class UpdatePasswordUserValidator : BaseValidator<UpdatePasswordUserModel>, IValidatorModel<UpdatePasswordUserModel>
{
    public UpdatePasswordUserValidator()
    {
        RuleFor(c => c.Password).NotNullOrEmpty().WithError(UserErrors.Validators.PASSWORD_REQUIRED);

        When(c => !string.IsNullOrWhiteSpace(c.Password), () =>
        {
            RuleFor(c => c.Password.Length).GreaterThanOrEqualTo(6).WithError(UserErrors.Validators.PASSWORD_LENGTH);
        });
    }
}
