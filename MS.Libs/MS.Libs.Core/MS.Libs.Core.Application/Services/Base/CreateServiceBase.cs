using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Libs.Core.Application.Services.Base;

public class CreateServiceBase<TModel, TEntity> : BaseService, ICreateService<TModel> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    protected readonly IValidatorModel<TModel> _validatorModel;
    protected readonly ICreateRepository<TEntity> _createRepository;
    protected readonly IMapperPlugin _imapper;

    public CreateServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _validatorModel = serviceProvider.GetService<IValidatorModel<TModel>>();
        _createRepository = serviceProvider.GetService<ICreateRepository<TEntity>>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
    }

    public virtual async Task<TModel> ExecuteAsync(TModel param)
    {
        return await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var entity = _imapper.Map<TEntity>(param);

            var retorno = await _createRepository.CreateAsync(entity);

            ValidateAddEntity(retorno);

            return _imapper.Map<TModel>(retorno);
        });
    }

    protected virtual async Task ValidateAsync(TModel param)
    {
        await _validatorModel.ValidateModelAsync(param);
    }

    protected void ValidateAddEntity(BaseEntityBasic entityBasic)
    {
        if (entityBasic == null || string.IsNullOrEmpty(entityBasic.Id.ToString()))
        {
            throw new BusinessException("Algo não ocorreu bem ao persistir a entidade");
        }
    }
}

