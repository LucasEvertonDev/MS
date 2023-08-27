using AutoMapper;
using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Students.Core.Domain.DbContexts.Entities;
using MS.Services.Students.Core.Domain.Models.Students;

namespace MS.Services.Students.Plugins.AutoMapper.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        ConvertDomainToModel();

        ConvertModelToDomain();
    }

    public void ConvertDomainToModel()
    {
        CreateMap<Student, CreatedStudentModel>().ReverseMap();
        CreateMap<Student, UpdatedStudentModel>().ReverseMap();
        CreateMap<Student, SearchedStudentModel>().ReverseMap();
        CreateMap<PagedResult<Student>, PagedResult<SearchedStudentModel>>().ReverseMap();
    }

    public void ConvertModelToDomain()
    {
        CreateMap<CreateStudentModel, Student>().ReverseMap();
        CreateMap<UpdateStudentModel, Student>().ReverseMap();
    }
}
