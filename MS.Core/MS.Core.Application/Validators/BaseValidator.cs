using FluentValidation;
using MS.Core.Domain.Models.Base;
using MS.Core.Domain.Services.Validators;
using MS.Infra.Utils.Exceptions;

namespace MS.Core.Application.Validators;

public class BaseValidator<TModel> : AbstractValidator<TModel>, IValidatorModel<TModel> where TModel : IModel
{
    public async Task ValidateModelAsync(TModel model)
    {
        var validationResult = await this.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            throw new BusinessException(validationResult.Errors.Select(c => c.ErrorMessage).ToArray());
        }
    }
}
