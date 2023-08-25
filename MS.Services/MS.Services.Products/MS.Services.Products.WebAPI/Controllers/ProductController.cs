using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Services.Products.Core.Application.Services.Products;
using MS.Services.Products.Core.Domain.Models.Auth;
using MS.Services.Products.Core.Domain.Services.ProductsService;

namespace MS.Services.Products.WebAPI.Controllers;

[Route("api/v1/products")]
public class ProductController : BaseController
{
    private readonly ICreateProductSetvice _createProductSetvice;
    private readonly IUpdateProductService _updateProductService;

    public ProductController(ICreateProductSetvice createUserService,
        IUpdateProductService updateProductService)
    {
        _createProductSetvice = createUserService;
        _updateProductService = updateProductService;
    }

    [Authorize]
    [HttpPost("create")]
    [ProducesResponseType(typeof(CreatedProductModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post([FromBody] CreateProductModel createProductModel)
    {
        await _createProductSetvice.ExecuteAsync(createProductModel);

        return Ok(_createProductSetvice.CreatedProduct);
    }

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(CreatedProductModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Put()
    {
        await _updateProductService.ExecuteAsync(new Libs.Core.Domain.Models.Base.BaseModel());
        return Ok(new CreatedProductModel());
    }
}