using MS.Libs.Core.Application.Services.Base;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Application.Services.Common;

public class CreateService<TModel, TEntity> : CreateServiceBase<TModel, TEntity> where TModel : BaseModel where TEntity : BaseEntityBasic
{
    public CreateService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
