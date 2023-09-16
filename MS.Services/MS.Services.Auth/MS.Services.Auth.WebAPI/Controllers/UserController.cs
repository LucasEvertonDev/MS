using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Models.Dto;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.Infrastructure.Attributes;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Services.UserServices;
using Prometheus;

namespace MS.Services.Auth.WebAPI.Controllers;

[Route("api/v1/users")]
public class UsersController : BaseController
{
    private readonly ICreateUserService _createUserService;
    private readonly IUpdateUserService _updateUserService;
    private readonly IDeleteUserService _deleteUserService;
    private readonly ISearchUserService _searchServices;
    private readonly IUpdatePasswordService _updatePasswodService;

    public UsersController(ICreateUserService createUserService,
        IUpdateUserService updateUserService,
        IDeleteUserService deleteUserService,
        ISearchUserService searchServices,
        IUpdatePasswordService updatePasswordService)
    {
        _createUserService = createUserService;
        _updateUserService = updateUserService;
        _deleteUserService = deleteUserService;
        _searchServices = searchServices;
        _updatePasswodService = updatePasswordService;
    }

    [HttpGetParams<SeacrhUserDto>, Authorize]
    [ProducesResponseType(typeof(ResponseDto<PagedResult<SearchedUserModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(SeacrhUserDto seacrhUserDto)
    {
        await _searchServices.ExecuteAsync(seacrhUserDto);

        return Ok(new ResponseDto<PagedResult<SearchedUserModel>>()
        {
            Content = _searchServices.SearchedUsers
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto<CreatedUserModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post([FromBody] CreateUserModel createUserModel)
    {
        await _createUserService.ExecuteAsync(createUserModel);

        return Ok(new ResponseDto<CreatedUserModel>()
        {
            Content = _createUserService.CreatedUser
        });
    }

    [HttpPut("{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto<UpdatedUserModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Put(UpdateUserDto updateUserModel)
    {
        await _updateUserService.ExecuteAsync(updateUserModel);

        return Ok(new ResponseDto<UpdatedUserModel>()
        {
            Content= _updateUserService.UpdatedUser
        });
    }

    [HttpPut("updatepassword/{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordUserDto passwordDto)
    {
        await _updatePasswodService.ExecuteAsync(passwordDto);

        return Ok(new ResponseDto()
        {
            Success = true
        });
    }

    [HttpDelete("{id}"), Authorize]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(DeleteUserDto deleteUserDto)
    {
        await _deleteUserService.ExecuteAsync(deleteUserDto);

        return Ok(new ResponseDto()
        {
            Success = true
        });
    }
}
