using AutoMapper;
using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Plugins.AutoMapper.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        ConvertDomainToModel();

        ConvertModelToDomain();
    }

    public void ConvertDomainToModel()
    {
        CreateMap<User, CreatedUserModel>().ReverseMap();
        CreateMap<User, UpdatedUserModel>().ReverseMap();
        CreateMap<User, SearchedUserModel>().ReverseMap();
        CreateMap<PagedResult<User>, PagedResult<SearchedUserModel>>().ReverseMap();
    }

    public void ConvertModelToDomain()
    {
        CreateMap<CreateUserModel, User>().ReverseMap();
        CreateMap<UpdateUserModel, User>().ReverseMap();
    }
}
