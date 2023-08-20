using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Core.Domain.Services;

namespace MS.Services.Auth.Core.Application.Base.Crud;

public class CreateService<TModel, TEntity> : BaseService, IBaseService<TModel, TModel> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    private readonly IValidatorModel<TModel> _validatorModel;
    private readonly ICreateRepository<TEntity> _createRepository;
    private readonly IMapperPlugin _imapper;

    public CreateService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _validatorModel = serviceProvider.GetService<IValidatorModel<TModel>>();
        _createRepository = serviceProvider.GetService<ICreateRepository<TEntity>>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
    }

    public async Task<TModel> ExecuteAsync(TModel param)
    {
        return await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var entity = _imapper.Map<TEntity>(param);

            var retorno = await _createRepository.CreateAsync(entity);

            ValidatePersistedEntity(retorno);

            return _imapper.Map<TModel>(retorno);
        });
    }

    public async Task ValidateAsync(TModel param)
    {
        await _validatorModel.ValidateModelAsync(param);
    }
}
