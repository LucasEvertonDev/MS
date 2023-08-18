using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Plugins.Validators;

public interface IValidatorModel<TModel> where TModel : IModel
{
    Task ValidateModelAsync(TModel model);
}
