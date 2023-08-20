using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.Infra.Utils.Activator;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Libs.Core.Application.Services.Base;

public class DeleteServiceBase<TModel, TEntity> : BaseService, IDeleteService<TModel> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    protected readonly IDeleteRepository<TEntity> _deleteRepository;
    protected readonly ISearchRepository<TEntity> _searchRepository;
    protected readonly IMapperPlugin _imapper;

    public DeleteServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _deleteRepository = serviceProvider.GetService<IDeleteRepository<TEntity>>();
        _searchRepository = serviceProvider.GetService<ISearchRepository<TEntity>>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
    }

    public virtual async Task ExecuteAsync(string id)
    {
        await OnTransactionAsync(async () =>
        {
            var param = App.Init<TModel>();
            param.Id = id;

            await DeleteAsync(param);
        });
    }

    public virtual async Task ExecuteAsync(TModel param)
    {
        await OnTransactionAsync(async () =>
        {
            await DeleteAsync(param);
        });
    }

    protected virtual async Task ValidateAsync(TModel param)
    {
        var user = await _searchRepository.FirstOrDefaultAsync(a => a.Id.ToString() == param.Id);

        ValidateSearchEntityId(user);
    }

    protected void ValidateUpdateEntity(BaseEntityBasic entityBasic)
    {
        if (entityBasic == null || string.IsNullOrEmpty(entityBasic.Id.ToString()))
        {
            throw new BusinessException("Algo não ocorreu bem ao persistir a entidade");
        }
    }

    private async Task DeleteAsync(TModel param)
    {
        await ValidateAsync(param);

        var entity = _imapper.Map<TEntity>(param);

        await _deleteRepository.DeleteAsync(a => a.Id.ToString() == param.Id);
    }
}
