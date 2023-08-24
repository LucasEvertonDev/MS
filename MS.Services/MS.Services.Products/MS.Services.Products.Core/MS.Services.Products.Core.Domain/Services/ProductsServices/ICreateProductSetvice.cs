using MS.Services.Products.Core.Domain.Models.Auth;

namespace MS.Services.Products.Core.Domain.Services.ProductsService;

public interface ICreateProductSetvice
{
    CreatedProductModel CreatedProduct { get; set; }

    Task ExecuteAsync(CreateProductModel param);
}