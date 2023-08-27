using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.Infrastructure.Attributes;
using MS.Services.Students.Core.Domain.Models.Students;
using MS.Services.Students.Core.Domain.Services.StudentServices;

namespace MS.Services.Students.WebAPI.Controllers;

[Route("api/v1/students")]
public class StudentsController : BaseController
{
    private readonly ICreateStudentService _createStudentService;
    private readonly IUpdateStudentService _updateStudentService;
    private readonly IDeleteStudentService _deleteStudentService;
    private readonly ISearchStudentService _searchServices;

    public StudentsController(ICreateStudentService createStudentService,
        IUpdateStudentService updateStudentService,
        IDeleteStudentService deleteStudentService,
        ISearchStudentService searchServices)
    {
        _createStudentService = createStudentService;
        _updateStudentService = updateStudentService;
        _deleteStudentService = deleteStudentService;
        _searchServices = searchServices;
    }

    [HttpGetParams<SeacrhStudentDto>, Authorize]
    [ProducesResponseType(typeof(PagedResult<SearchedStudentModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(SeacrhStudentDto seacrhStudentDto)
    {
        await _searchServices.ExecuteAsync(seacrhStudentDto);

        return Ok(_searchServices.SearchedStudents);
    }

    [HttpPost()]
    [Authorize(Roles = "CHANGE_STUDENTS")]
    [ProducesResponseType(typeof(CreatedStudentModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post([FromBody] CreateStudentModel createStudentModel)
    {
        await _createStudentService.ExecuteAsync(createStudentModel);

        return Ok(_createStudentService.CreatedStudent);
    }

    [Authorize(Roles = "CHANGE_STUDENTS")]
    [HttpPut("{id}"), Authorize]
    [ProducesResponseType(typeof(UpdatedStudentModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Put(UpdateStudentDto updateStudentModel)
    {
        await _updateStudentService.ExecuteAsync(updateStudentModel);

        return Ok(_updateStudentService.UpdatedStudent);
    }

    [Authorize(Roles = "CHANGE_STUDENTS")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DeletedStudentModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(DeleteStudentDto deleteStudentDto)
    {
        await _deleteStudentService.ExecuteAsync(deleteStudentDto);

        return Ok(new DeletedStudentModel
        {
            Sucess = true
        });
    }
}
