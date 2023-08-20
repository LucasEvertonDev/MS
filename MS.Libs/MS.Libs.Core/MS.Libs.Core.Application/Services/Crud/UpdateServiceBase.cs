using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Libs.Core.Application.Services.Crud
{
    public class UpdateServiceBase<TModel, TEntity> : BaseService, IUpdateService<TModel> where TModel : BaseModel where TEntity : BaseEntityBasic
    {
        private readonly IValidatorModel<TModel> _validatorModel;
        private readonly IUpdateRepository<TEntity> _updateRepository;
        private readonly ISearchRepository<TEntity> _searchRepository;
        private readonly IMapperPlugin _imapper;

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
                await ValidateAsync(id, param);
               
                var entity = _imapper.Map<TEntity>(param);

                var retorno = await _updateRepository.Update(entity);

                ValidateUpdateEntity(retorno);

                return _imapper.Map<TModel>(retorno);
            });
        }

        protected virtual async Task ValidateAsync(string id, TModel param)
        {
            var user = await _searchRepository.FindById(a => a.Id.ToString() == id);

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
    }
}
