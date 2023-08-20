using FluentValidation;
using FluentValidation.Results;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Infra.Plugins.FluentValidation.CustomValidators;

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
                    contexto.AddFailure(new ValidationFailure(nameof(UserModel.Username),"Login inválido"));
                }

                if (username.Length < 10)
                {
                    contexto.AddFailure(new ValidationFailure(nameof(UserModel.Username), "Login deve ter x caracteres"));
                }
            });
        });
    }
}
