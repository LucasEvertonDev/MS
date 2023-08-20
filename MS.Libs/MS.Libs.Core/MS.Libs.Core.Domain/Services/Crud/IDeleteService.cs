using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Services.Crud;

public interface IDeleteService<TModel> where TModel : BaseModel
{
    Task ExecuteAsync(string id);
    Task ExecuteAsync(TModel param);
}