using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.Infrastructure.Attributes;
using MS.Services.Gateway.Core.Domain.Models.Courses;
using MS.Services.Gateway.Core.Domain.Models.Students;
using MS.Services.Gateway.Plugins.Redit;

namespace MS.Services.Gateway.WebAPI.Controllers;

[Route("apigateway/v1/students")]
public class StudentsController : BaseController
{
    private readonly IStudentsApi _studentsApi;

    public StudentsController(IStudentsApi studentsApi)
    {
        _studentsApi = studentsApi;
    }

    [HttpGetParams<SeacrhStudentDto>, Authorize]
    [ProducesResponseType(typeof(PagedResult<SearchedStudentModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(SeacrhStudentDto seacrhStudentDto)
    {
        var retorno = await _studentsApi.Get(seacrhStudentDto.Token, seacrhStudentDto.PageNumber, seacrhStudentDto.PageSize, new Plugins.Redit.ApiDtos.Courses.ApiSearchStudentsDto
        {
            Name = seacrhStudentDto.Name,
        });
        return Ok(retorno);
    }

    [HttpPost()]
    [Authorize(Roles = "CHANGE_STUDENTS")]
    [ProducesResponseType(typeof(CreatedStudentModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post(CreateStudentsDto createStudentsModel)
    {
        var retorno = await _studentsApi.Post(createStudentsModel.Token, new Plugins.Redit.ApiDtos.Courses.ApiCreateStudentsDto
        {
            Name = createStudentsModel.Body.Name,
            Cpf = createStudentsModel.Body.Cpf,
            FatherName = createStudentsModel.Body.FatherName,
            MotherName = createStudentsModel.Body.MotherName
        });

        return Ok(retorno);
    }

    [HttpPut("{id}"), Authorize(Roles = "CHANGE_STUDENTS")]
    [ProducesResponseType(typeof(UpdatedStudentModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Put(UpdateStudentDto updateStudentDto)
    {
        var retorno = await _studentsApi.Put(updateStudentDto.Token, updateStudentDto.Id, new Plugins.Redit.ApiDtos.Courses.ApiUpdateStudentsDto
        {
            Name = updateStudentDto.Body.Name,
            Cpf = updateStudentDto.Body.Cpf,
            FatherName = updateStudentDto.Body.FatherName,
            MotherName = updateStudentDto.Body.MotherName
        });

        return Ok(retorno);
    }

    [Authorize(Roles = "CHANGE_STUDENTS")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DeletedStudentModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(DeleteStudentDto deleteStudentDto)
    {
        await _studentsApi.Delete(deleteStudentDto.Token, deleteStudentDto.Id);

        return Ok(new DeletedCourseModel
        {
            Sucess = true
        });
    }
}
