using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Services.Crud
{
    public interface ISeachService<TModel> where TModel : BaseModel
    {
        Task<TModel> FirstOrDefault(string id);

        Task<List<TModel>> ToListAsync();
    }
}