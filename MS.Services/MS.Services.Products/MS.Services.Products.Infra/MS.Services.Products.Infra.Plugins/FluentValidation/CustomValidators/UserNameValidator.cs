using FluentValidation;
using FluentValidation.Results;
using MS.Services.Products.Core.Domain.Models.Users;

namespace MS.Services.Products.Infra.Plugins.FluentValidation.CustomValidators;

internal class UserNameValidator : AbstractValidator<string>
{
    public UserNameValidator()
    {
        RuleFor(UserName => UserName).NotEmpty().WithMessage("Username é obrigatorio");
        When(UserName => !string.IsNullOrWhiteSpace(UserName), () =>
        {
            RuleFor(UserName => UserName).Custom((username, contexto) =>
            {
                if (username.Contains(" "))
                {
                    contexto.AddFailure(new ValidationFailure(nameof(CreatedUserModel.Username),"Login inválido"));
                }

                if (username.Length > 20)
                {
                    contexto.AddFailure(new ValidationFailure(nameof(CreatedUserModel.Username), "Login deve ter no máximo 20 caracteres"));
                }
            });
        });
    }
}
