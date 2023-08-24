using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Services.Products.Core.Domain.Models.Auth;
using MS.Services.Products.Core.Domain.Services.ProductsService;

namespace MS.Services.Products.WebAPI.Controllers;

[Route("api/v1/products")]
public class ProductController : BaseController
{
    private readonly ICreateProductSetvice _createProductSetvice;

    public ProductController(ICreateProductSetvice createUserService)
    {
        _createProductSetvice = createUserService;
    }

    [Authorize]
    [HttpPost("create")]
    [ProducesResponseType(typeof(CreatedProductModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Create(CreateProductModel createProductModel)
    {
        await _createProductSetvice.ExecuteAsync(createProductModel);

        return Ok(_createProductSetvice.CreatedProduct);
    }


    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(CreatedProductModel), StatusCodes.Status200OK)]
    public ActionResult Put([FromBody] CreatedProductModel loginModel)
    {
        return Ok(new CreatedProductModel());
    }
}