using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;

namespace MS.Services.Products.Core.Domain.Models.Auth;

public class CreateProductModel : BaseModel
{
    [DefaultValue("Caderno")]
    public string Name { get; set; }
    [DefaultValue("Caderno de escrita")]
    public string Description { get; set; }
    [DefaultValue("12.2")]
    public decimal Price { get; set; }
}


public class CreatedProductModel : CreateProductModel
{
    [DefaultValue("1281298291289djjjdjd23")]
    public string Id { get; set; }
}