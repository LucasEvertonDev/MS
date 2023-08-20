using AutoMapper;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Infra.Plugins.AutoMapper.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        ConvertDomainToModel();

        ConvertModelToDomain();
    }


    public void ConvertDomainToModel()
    {
        CreateMap<User, UserModel>();
    }

    public void ConvertModelToDomain()
    {
        CreateMap<UserModel, User>();
    }
}
