using FluentValidation;
using FluentValidation.Results;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Plugins.FluentValidation.CustomValidators;

namespace MS.Services.Auth.Plugins.FluentValidation.User;

public class CreateUserValidator : BaseValidator<CreateUserModel>, IValidatorModel<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Username).NotNull().WithMessage(UserErrors.USERNAME_REQUIRED.ErrorMessage).WithErrorCode(UserErrors.USERNAME_REQUIRED.ErrorCode);
        RuleFor(c => c.Email).NotEmpty().WithMessage(UserErrors.EMAIL_INVALID.ErrorMessage).WithErrorCode(UserErrors.EMAIL_INVALID.ErrorCode);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(UserErrors.EMAIL_INVALID.ErrorMessage).WithErrorCode(UserErrors.EMAIL_INVALID.ErrorCode);
        });
    }
}


public class BaseValidator<TModel> : AbstractValidator<TModel>, IValidatorModel<TModel> where TModel : IModel
{
    public async Task ValidateModelAsync(TModel model)
    {
        ValidationResult validationResult = await ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            throw new BusinessException(validationResult.Errors.Select((ValidationFailure c) => new ErrorModel(c.ErrorMessage, c.ErrorCode)).ToArray());
        }
    }
}