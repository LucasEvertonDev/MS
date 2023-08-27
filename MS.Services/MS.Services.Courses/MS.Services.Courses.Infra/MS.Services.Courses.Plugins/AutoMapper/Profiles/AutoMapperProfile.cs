using AutoMapper;
using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Courses.Core.Domain.DbContexts.Entities;
using MS.Services.Courses.Core.Domain.Models.Courses;

namespace MS.Services.Courses.Plugins.AutoMapper.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        ConvertDomainToModel();

        ConvertModelToDomain();
    }

    public void ConvertDomainToModel()
    {
        CreateMap<Course, CreatedCourseModel>().ReverseMap();
        CreateMap<Course, UpdatedCourseModel>().ReverseMap();
        CreateMap<Course, SearchedCourseModel>().ReverseMap();
        CreateMap<PagedResult<Course>, PagedResult<SearchedCourseModel>>().ReverseMap();
    }

    public void ConvertModelToDomain()
    {
        CreateMap<CreateCourseModel, Course>().ReverseMap();
        CreateMap<UpdateCourseModel, Course>().ReverseMap();
    }
}
