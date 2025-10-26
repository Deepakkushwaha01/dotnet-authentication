using Authentication.API.DTOs;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Core.Commands.Auth.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public ClaimsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] AddRoleClaimsDto request)
    {
        var result = await _commandDispatcher.ExecuteAsync(new AddRoleClaimsCommand(request));

        if (!result.IsSuccess)
        {
            ObjectResult errorResponse = StatusCode(((int)result.HttpStatusCode), result.Message);
            return errorResponse;
        }

        return Ok(result);
    }
}