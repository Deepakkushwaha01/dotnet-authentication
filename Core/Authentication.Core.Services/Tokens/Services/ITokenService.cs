
using System.Security.Claims;
using Authentication.Common.Result;

namespace Authentication.Core.Services.TokenService
{
    public interface ITokenService
    {
        Result<string> GenerateAccessToken(Guid userId, string email, IList<string> roles);
        Result<string> GenerateRefreshToken(Guid userId, string email);
        Result<ClaimsPrincipal> ValidateRefreshToken(string encodedToken);

        Result<ClaimsPrincipal> ValidateAccessToken(string token);
    }
}
