using MS.Libs.Core.Application.Services.Base;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Libs.Core.Application.Services.Common;

public class UpdateService<TModel, TEntity> : UpdateServiceBase<TModel, TEntity> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    public UpdateService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
