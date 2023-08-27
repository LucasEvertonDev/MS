using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.Infrastructure.Attributes;
using MS.Services.Gateway.Core.Domain.Models.Courses;
using MS.Services.Gateway.Plugins.Redit.CoursesApi;

namespace MS.Services.Gateway.WebAPI.Controllers;

[Route("apigateway/v1/courses")]
public class CoursesController : BaseController
{
    private readonly ICoursesApi _coursesApi;

    public CoursesController(ICoursesApi coursesApi)
    {
        _coursesApi = coursesApi;
    }

    [HttpGetParams<SeacrhCourseDto>, Authorize]
    [ProducesResponseType(typeof(PagedResult<SearchedCourseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(SeacrhCourseDto seacrhCourseDto)
    {
        return Ok(await _coursesApi.Get(seacrhCourseDto.Token, seacrhCourseDto.PageNumber, seacrhCourseDto.PageSize, new Plugins.Redit.ApiDtos.Courses.ApiSearchCoursesDto 
        {
            Name = seacrhCourseDto.Name,
        }));
    }

    //[HttpPost()]
    //[Authorize()]
    //[ProducesResponseType(typeof(CreatedCourseModel), StatusCodes.Status200OK)]
    //public async Task<ActionResult> Post([FromBody] CreateCourseModel createCourseModel)
    //{
    //    await _createCourseservice.ExecuteAsync(createCourseModel);

    //    return Ok(_createCourseservice.CreatedCourse);
    //}

    //[Authorize()]
    //[HttpPut("{id}"), Authorize]
    //[ProducesResponseType(typeof(UpdatedCourseModel), StatusCodes.Status200OK)]
    //public async Task<ActionResult> Put(UpdateCourseDto updateCourseModel)
    //{
    //    await _updateCourseservice.ExecuteAsync(updateCourseModel);

    //    return Ok(_updateCourseservice.UpdatedCourse);
    //}

    //[Authorize()]
    //[HttpDelete("{id}")]
    //[ProducesResponseType(typeof(DeletedCourseModel), StatusCodes.Status200OK)]
    //public async Task<ActionResult> Delete(DeleteCourseDto deleteCourseDto)
    //{
    //    await _deleteCourseservice.ExecuteAsync(deleteCourseDto);

    //    return Ok(new DeletedCourseModel
    //    {
    //        Sucess = true
    //    });
    //}
}
