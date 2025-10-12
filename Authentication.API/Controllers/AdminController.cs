using Authentication.API.DTOs;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Core.Commands.Auth.Users;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AdminController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateAdminRequestDto request)
    {
        var result = await _commandDispatcher.ExecuteAsync(new AddAdminCommand(request));

        if (!result.IsSuccess)
        {
            ObjectResult errorResponse = StatusCode(((int)result.HttpStatusCode), result.Message);
            return errorResponse;
        }

        return Ok(result.Value);
    }
}