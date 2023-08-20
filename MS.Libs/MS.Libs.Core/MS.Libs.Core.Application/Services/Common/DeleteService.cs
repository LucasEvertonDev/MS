using MS.Libs.Core.Application.Services.Base;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Application.Services.Common;

public class DeleteService<TModel, TEntity> : DeleteServiceBase<TModel, TEntity> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    public DeleteService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
