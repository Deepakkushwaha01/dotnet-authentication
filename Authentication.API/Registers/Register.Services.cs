using Authentication.Core.Persistence.Admin.Repo;

namespace Authentication.API.Registers;

public static partial class Register
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        // Register application services here
        // services.AddScoped<IYourService, YourServiceImplementation>();

        #region Admin Repositories For Dependency Injection
        services.AddScoped<IAdminRepo, AdminRepo>();
        #endregion

        return services;
    }
}