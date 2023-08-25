using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Products.Core.Domain.Models.Auth;

namespace MS.Services.Products.Core.Application.Services.Products
{
    public interface IUpdateProductService
    {
        CreatedProductModel CreatedProduct { get; set; }

        Task ExecuteAsync(BaseModel model);
    }
}