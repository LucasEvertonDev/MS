using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Services.Products.Core.Domain.DbContexts.Entities;
using MS.Services.Products.Core.Domain.Models.Auth;
using MS.Services.Products.Core.Domain.Services.ProductsService;

namespace MS.Services.Products.Core.Application.Services.Products;

public class UpdateProductService : BaseService<BaseModel>, IUpdateProductService
{
    private readonly IUpdateRepository<Product> _updateRepository;
    private readonly IValidatorModel<CreateProductModel> _validatorModel;
    private readonly ISearchRepository<Product> _search;

    public CreatedProductModel CreatedProduct { get; set; }

    public UpdateProductService(IServiceProvider serviceProvider,
        IUpdateRepository<Product> updateRepository,
        IValidatorModel<CreateProductModel> validatorModel,
        ISearchRepository<Product> search) : base(serviceProvider)
    {
        _updateRepository = updateRepository;
        _validatorModel = validatorModel;
        _search = search;
    }

    public async override Task ExecuteAsync(BaseModel model)
    {
        await OnTransactionAsync(async () =>
        {
            var result = await _search.FirstOrDefaultAsync(a => !a.UpdateDate.HasValue);
            var productCreated = await _updateRepository.UpdateAsync(result);

            CreatedProduct = _imapper.Map<CreatedProductModel>(productCreated);
        });
    }
}
