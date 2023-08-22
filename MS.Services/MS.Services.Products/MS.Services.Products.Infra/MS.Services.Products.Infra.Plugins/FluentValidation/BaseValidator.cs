using FluentValidation;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;

namespace MS.Services.Products.Infra.Plugins.FluentValidation;

public class BaseValidator<TModel> : AbstractValidator<TModel>, IValidatorModel<TModel> where TModel : BaseModel
{
    public async Task ValidateModelAsync(TModel model)
    {
        var validationResult = await ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            throw new BusinessException(validationResult.Errors.Select(c => c.ErrorMessage).ToArray());
        }
    }
}
