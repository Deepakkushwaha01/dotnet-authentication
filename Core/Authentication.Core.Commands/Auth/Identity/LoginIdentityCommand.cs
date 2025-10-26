using Authentication.API.DTOs;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Common.Result;
using Authentication.Core.Domain.Users;
using Authentication.Core.Persistence.IdentityRepo.Repo;
using Authentication.Core.Services.TokenService;
using Authentication.Core.SharedKernel.Enums;

namespace Authentication.Core.Commands.Auth.Identity
{
    public class LoginIdentityCommand : ICommand<Result<LoginIdentityResponseDto>>
    {
        public LoginIdentityRequestDto _request;

        public LoginIdentityCommand(LoginIdentityRequestDto requests)
        {
            _request = requests;
        }

    }

    public class LoginIdentityCommandHandler(
        IIdentityRepo _identityRepo,
        ITokenService _tokenService
    ) : ICommandHandler<LoginIdentityCommand, Result<LoginIdentityResponseDto>>
    {
        public async Task<Result<LoginIdentityResponseDto>> ExecuteAsync(LoginIdentityCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _identityRepo.GetUserByEmailOrUid(command._request.Email, null, new List<string> { IdentityTable.IdentityUserRoles.ToString() });
                if (result.IsFailure)
                {
                    return Result.FromError<LoginIdentityResponseDto>(result);
                }

                var refreshToken = _tokenService.GenerateRefreshToken(result.Value.Uid, result.Value.Email);

                if (refreshToken.IsFailure)
                {
                    return Result.Invalid<LoginIdentityResponseDto>("Failed to generate refresh token.");
                }

                // var accessToken = _tokenService.GenerateAccessToken(result.Value.Uid, result.Value.Email, result.Value.Roles);


                return Result.Ok(new LoginIdentityResponseDto
                {
                    Uid = result.Value.Uid,
                    Email = result.Value.Email,
                    FirstName = result.Value.FirstName,
                    LastName = result.Value.LastName
                });
            }
            catch (System.Exception ex)
            {

                return Result.Invalid<LoginIdentityResponseDto>($"Failed to Login user {ex.Message}");
            }
        }
    }
}