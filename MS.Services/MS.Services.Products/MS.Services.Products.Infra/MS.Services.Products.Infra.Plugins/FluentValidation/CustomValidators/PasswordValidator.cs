using FluentValidation;

namespace MS.Services.Products.Infra.Plugins.FluentValidation.CustomValidators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(senha => senha).NotEmpty().WithMessage("Senha obrigatoria");
        When(senha => !string.IsNullOrWhiteSpace(senha), () =>
        {
            RuleFor(senha => senha.Length).GreaterThanOrEqualTo(6).WithMessage("Senha deve ter no minimo seis carcteres");
        });
    }
}
