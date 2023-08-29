using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Models.Dto;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.Infrastructure.Attributes;
using MS.Services.Courses.Core.Domain.Models.Courses;
using MS.Services.Courses.Core.Domain.Services.Courseservices;

namespace MS.Services.Courses.WebAPI.Controllers;

[Route("api/v1/courses")]
public class CoursesController : BaseController
{
    private readonly ICreateCourseservice _createCourseservice;
    private readonly IUpdateCourseservice _updateCourseservice;
    private readonly IDeleteCourseservice _deleteCourseservice;
    private readonly ISearchCourseservice _searchServices;

    public CoursesController(ICreateCourseservice createCourseservice,
        IUpdateCourseservice updateCourseservice,
        IDeleteCourseservice deleteCourseservice,
        ISearchCourseservice searchServices)
    {
        _createCourseservice = createCourseservice;
        _updateCourseservice = updateCourseservice;
        _deleteCourseservice = deleteCourseservice;
        _searchServices = searchServices;
    }

    [HttpGetParams<SeacrhCourseDto>, Authorize]
    [ProducesResponseType(typeof(ResponseDto<PagedResult<SearchedCourseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(SeacrhCourseDto seacrhCourseDto)
    {
        await _searchServices.ExecuteAsync(seacrhCourseDto);

        return Ok(new ResponseDto<PagedResult<SearchedCourseModel>>()
        { 
            Content = _searchServices.SearchedCourses
        });
    }

    [HttpPost()]
    [Authorize()]
    [ProducesResponseType(typeof(ResponseDto<CreatedCourseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post([FromBody] CreateCourseModel createCourseModel)
    {
        await _createCourseservice.ExecuteAsync(createCourseModel);

        return Ok(new ResponseDto<CreatedCourseModel>()
        { 
            Content = _createCourseservice.CreatedCourse
        });
    }

    [Authorize()]
    [HttpPut("{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto<UpdatedCourseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Put(UpdateCourseDto updateCourseModel)
    {
        await _updateCourseservice.ExecuteAsync(updateCourseModel);

        return Ok(new ResponseDto<UpdatedCourseModel>()
        {
            Content = _updateCourseservice.UpdatedCourse
        });
    }

    [Authorize()]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(DeleteCourseDto deleteCourseDto)
    {
        await _deleteCourseservice.ExecuteAsync(deleteCourseDto);

        return Ok(new ResponseDto
        {
            Success = true
        });
    }
}
