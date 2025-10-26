using Microsoft.Extensions.Configuration;

namespace Authentication.Core.Services.Settings
{
    public class JwtStringSettings(IConfiguration configuration)
    {
        public string AccessTokenSecret { get; } = configuration.GetValue<string>("JWT:AccessTokenSecret")!;

        public string RefreshTokenSecret { get; } = configuration.GetValue<string>("JWT:RefreshTokenSecret")!;

        public double AccessTokenExpiry { get; } = configuration.GetValue<double>("JWT:AccessTokenExpiry")!;

        public double RefreshTokenExpiry { get; } = configuration.GetValue<double>("JWT:RefreshTokenExpiry")!;
        public string Issuer { get; } = configuration.GetValue<string>("JWT:Issuer")!;
        public string Audience { get; } = configuration.GetValue<string>("JWT:Audience")!;
    }
}