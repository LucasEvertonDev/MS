using FluentValidation;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;

namespace MS.Libs.Infra.Plugins.Validators;

public class BaseValidator<TModel> : AbstractValidator<TModel>, IValidatorModel<TModel> where TModel : IModel
{
    public async Task ValidateModelAsync(TModel model)
    {
        var validationResult = await ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            throw new BusinessException(validationResult.Errors.Select(c => new ErrorModel(c.ErrorMessage, c.ErrorCode)).ToArray());
        }
    }
}
