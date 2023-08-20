using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Libs.Core.Application.Services.Base;

public class SearchServiceBase<TModel, TEntity> : BaseService, ISeachService<TModel> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    protected readonly ISearchRepository<TEntity> _searchRepository;
    protected readonly IMapperPlugin _imapper;

    public SearchServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _searchRepository = serviceProvider.GetService<ISearchRepository<TEntity>>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
    }

    public async Task<TModel> FirstOrDefault(string id)
    {
        return await OnTransactionAsync(async () =>
        {
            var item = await _searchRepository.FirstOrDefaultAsync(a => a.Id.ToString() == id);
            return _imapper.Map<TModel>(item);
        });
    }

    public async Task<List<TModel>> ToListAsync()
    {
        return await OnTransactionAsync(async () =>
        {
            var list = await _searchRepository.ToListAsync();
            return _imapper.Map<List<TModel>>(list);
        });
    }
}
