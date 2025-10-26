using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Common.Result;
using Authentication.Core.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Core.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly JwtStringSettings _jwtSettings;

        public TokenService(IConfiguration configuration)
        {
            _jwtSettings = new JwtStringSettings(configuration);
        }

        public Result<string> GenerateAccessToken(Guid userId, string email, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.AccessTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiry),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Result.Ok(Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenString)));
        }

        public Result<string> GenerateRefreshToken(Guid userId, string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var refreshToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiry),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            return Result.Ok(Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenString))); // encoded before DB save
        }

        // ✅ Validate Access Token
        public Result<ClaimsPrincipal> ValidateAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.AccessTokenSecret);

            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out _);
                return Result.Ok(principal);
            }
            catch
            {
                return Result.Invalid<ClaimsPrincipal>("Invalid access token.");
            }
        }

        public Result<ClaimsPrincipal> ValidateRefreshToken(string encodedToken)
        {
            try
            {
                var tokenString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedToken));

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenSecret))
                };

                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(tokenString, parameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwt &&
                    jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.OrdinalIgnoreCase))
                {
                    return Result.Ok(principal);
                }

                return Result.Invalid<ClaimsPrincipal>("Invalid refresh token.");
            }
            catch
            {
                return Result.Invalid<ClaimsPrincipal>("Invalid refresh token.");
            }
        }
    }
}
