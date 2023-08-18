using MS.Core.Domain.Models.Base;

namespace MS.Core.Domain.Services.Validators;

public interface IValidatorModel<TModel> where TModel : IModel
{
    Task ValidateModelAsync(TModel model);
}
