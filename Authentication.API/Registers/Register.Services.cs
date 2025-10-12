using Authentication.Core.Persistence.Users.Repo;

namespace Authentication.API.Registers;

public static partial class Register
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        // Register application services here
        // services.AddScoped<IYourService, YourServiceImplementation>();

        #region Repositories
        services.AddScoped<IUsersRepo, UsersRepo>();
        #endregion

        return services;
    }
}