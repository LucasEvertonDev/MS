using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Libs.Core.Application.Services.Base;

public class UpdateServiceBase<TModel, TEntity> : BaseService, IUpdateService<TModel> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    protected readonly IValidatorModel<TModel> _validatorModel;
    protected readonly IUpdateRepository<TEntity> _updateRepository;
    protected readonly ISearchRepository<TEntity> _searchRepository;
    protected readonly IMapperPlugin _imapper;

    public UpdateServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _validatorModel = serviceProvider.GetService<IValidatorModel<TModel>>();
        _updateRepository = serviceProvider.GetService<IUpdateRepository<TEntity>>();
        _searchRepository = serviceProvider.GetService<ISearchRepository<TEntity>>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
    }
    public virtual async Task<TModel> ExecuteAsync(string id, TModel param)
    {
        return await OnTransactionAsync(async () =>
        {
            param.Id = id;
            return await UpdateAsync(param);
        });
    }

    public virtual async Task<TModel> ExecuteAsync(TModel param)
    {
        return await OnTransactionAsync(async () =>
        {
            return await UpdateAsync(param);
        });
    }

    protected virtual async Task ValidateAsync(TModel param)
    {
        var user = await _searchRepository.FirstOrDefaultAsync(a => a.Id.ToString() == param.Id);

        ValidateSearchEntityId(user);

        await _validatorModel.ValidateModelAsync(param);
    }

    protected void ValidateUpdateEntity(BaseEntityBasic entityBasic)
    {
        if (entityBasic == null || string.IsNullOrEmpty(entityBasic.Id.ToString()))
        {
            throw new BusinessException("Algo não ocorreu bem ao persistir a entidade");
        }
    }

    private async Task<TModel> UpdateAsync(TModel param)
    {
        await ValidateAsync(param);

        var entity = _imapper.Map<TEntity>(param);

        var retorno = await _updateRepository.UpdateAsync(entity);

        ValidateUpdateEntity(retorno);

        return _imapper.Map<TModel>(retorno);
    }
}
