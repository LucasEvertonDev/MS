using MS.Libs.Core.Domain.Constants;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Products.Core.Domain.DbContexts.Entities;
using MS.Services.Products.Core.Domain.Models.Auth;
using MS.Services.Products.Core.Domain.Services.ProductsService;

namespace MS.Services.Products.Core.Application.Services.Products;

public class CreateProductsService : BaseService<CreateProductModel>, ICreateProductSetvice
{
    private readonly ICreateRepository<Product> _createProductRepository;
    private readonly IValidatorModel<CreateProductModel> _validatorModel;

    public CreatedProductModel CreatedProduct { get; set; }

    public CreateProductsService(IServiceProvider serviceProvider,
        ICreateRepository<Product> createProductRepository,
        IValidatorModel<CreateProductModel> validatorModel) : base(serviceProvider)
    {
        _createProductRepository = createProductRepository;
        _validatorModel = validatorModel;
    }

    public async override Task ExecuteAsync(CreateProductModel param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var product = _imapper.Map<Product>(param);
            product.Id = new Guid("95E48243-5328-4BFF-24F3-08DBA4C512F2");
            var productCreated = await _createProductRepository.CreateAsync(product);

            CreatedProduct = _imapper.Map<CreatedProductModel>(productCreated);
        });
    }

    protected override async Task ValidateAsync(CreateProductModel param)
    {
        await _validatorModel.ValidateModelAsync(param);
    }
}