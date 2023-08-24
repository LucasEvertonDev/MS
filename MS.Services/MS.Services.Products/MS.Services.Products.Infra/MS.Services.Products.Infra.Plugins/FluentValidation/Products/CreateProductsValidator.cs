using FluentValidation;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Plugins.Validators;
using MS.Services.Products.Core.Domain.Models.Auth;

namespace MS.Services.Products.Infra.Plugins.FluentValidation.User;

public class CreateProductsValidator : BaseValidator<CreateProductModel>, IValidatorModel<CreateProductModel>
{
    public CreateProductsValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name é obrigatorio");
    }
}
