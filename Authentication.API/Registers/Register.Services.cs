using Authentication.Core.Persistence.Database.Context;
using Authentication.Core.Persistence.IdentityRepo.Repo;
using Authentication.Core.Services.TokenService;

namespace Authentication.API.Registers;

public static partial class Register
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        // Register application services here
        // services.AddScoped<IYourService, YourServiceImplementation>();

        #region identity Repositories For Dependency Injection
        services.AddScoped<IIdentityRepo, IdentityRepo>();
        #endregion

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITokenService, TokenService>();


        return services;
    }
}