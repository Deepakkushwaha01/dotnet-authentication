using Authentication.API.DTOs;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Core.Commands.Auth.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public IdentityController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateIdentityRequestDto request)
    {
        var result = await _commandDispatcher.ExecuteAsync(new AddIdentityCommand(request));

        if (!result.IsSuccess)
        {
            ObjectResult errorResponse = StatusCode(((int)result.HttpStatusCode), result.Message);
            return errorResponse;
        }

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> loginUser([FromBody] LoginIdentityRequestDto request)
    {
        var result = await _commandDispatcher.ExecuteAsync(new LoginIdentityCommand(request));

        if (!result.IsSuccess)
        {
            ObjectResult errorResponse = StatusCode(((int)result.HttpStatusCode), result.Message);
            return errorResponse;
        }

        return Ok(result);
    }
}