using AutoMapper;
using MS.Services.Products.Core.Domain.DbContexts.Entities;
using MS.Services.Products.Core.Domain.Models.Auth;

namespace MS.Services.Products.Infra.Plugins.AutoMapper.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        ConvertDomainToModel();

        ConvertModelToDomain();
    }

    public void ConvertDomainToModel()
    {
        CreateMap<Product, CreateProductModel>().ReverseMap();
    }

    public void ConvertModelToDomain()
    {
        CreateMap<Product, CreatedProductModel>().ReverseMap();
    }
}
