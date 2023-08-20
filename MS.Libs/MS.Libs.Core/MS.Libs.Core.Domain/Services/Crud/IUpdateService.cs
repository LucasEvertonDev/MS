using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Services.Crud
{
    public interface IUpdateService<TModel> where TModel : BaseModel
    {
        Task<TModel> ExecuteAsync(string id, TModel param);

        Task<TModel> ExecuteAsync(TModel param);
    }
}