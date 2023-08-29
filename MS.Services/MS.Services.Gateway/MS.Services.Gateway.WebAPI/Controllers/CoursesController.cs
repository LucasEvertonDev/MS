using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Models.Dto;
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

    [HttpGetParams<SearchCourseDto>, Authorize]
    [ProducesResponseType(typeof(ResponseDto<PagedResult<SearchedCourseModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(SearchCourseDto seacrhCourseDto)
    {
        var retorno = await _coursesApi.Get(seacrhCourseDto.Token, seacrhCourseDto.PageNumber, seacrhCourseDto.PageSize, new Plugins.Redit.ApiDtos.Courses.ApiSearchCoursesDto
        {
            Name = seacrhCourseDto.Name,
        });
        return Ok(retorno);
    }

    [HttpPost()]
    [Authorize()]
    [ProducesResponseType(typeof(ResponseDto<CreatedCourseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post(CreateCourseDto createCourseModel)
    {
        var retorno = await _coursesApi.Post(createCourseModel.Token, new Plugins.Redit.ApiDtos.Courses.ApiCreateCoursesDto 
        {
            Name = createCourseModel.Body.Name,
            EndDate = createCourseModel.Body.EndDate,
            StartDate = createCourseModel.Body.StartDate,
        });

        return Ok(retorno);
    }

    [Authorize()]
    [HttpPut("{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto<UpdatedCourseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Put(UpdateCourseDto updateCourseModel)
    {
        var retorno = await _coursesApi.Put(updateCourseModel.Token, updateCourseModel.Id, new Plugins.Redit.ApiDtos.Courses.ApiUpdateCoursesDto
        {
            Name = updateCourseModel.Body.Name,
            StartDate = updateCourseModel.Body.StartDate,
            EndDate = updateCourseModel.Body.EndDate,
        });

        return Ok(retorno);
    }

    [Authorize()]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(DeleteCourseDto deleteCourseDto)
    {
        var retorno = await _coursesApi.Delete(deleteCourseDto.Token, deleteCourseDto.Id);

        return Ok(retorno);
    }
}
