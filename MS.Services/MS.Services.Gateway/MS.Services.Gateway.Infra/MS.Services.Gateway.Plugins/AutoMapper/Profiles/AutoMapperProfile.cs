using AutoMapper;
using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Gateway.Core.Domain.Models.Courses;

namespace MS.Services.Gateway.Plugins.AutoMapper.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //ConvertDomainToModel();

        //ConvertModelToDomain();
    }

    //public void ConvertDomainToModel()
    //{
    //    CreateMap<Course, CreatedCourseModel>().ReverseMap();
    //    CreateMap<Course, UpdatedCourseModel>().ReverseMap();
    //    CreateMap<Course, SearchedCourseModel>().ReverseMap();
    //    CreateMap<PagedResult<Course>, PagedResult<SearchedCourseModel>>().ReverseMap();
    //}

    //public void ConvertModelToDomain()
    //{
    //    CreateMap<CreateCourseModel, Course>().ReverseMap();
    //    CreateMap<UpdateCourseModel, Course>().ReverseMap();
    //}
}
