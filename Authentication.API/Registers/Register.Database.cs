using Authentication.API.Settings;
using Authentication.Core.Persistence.Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.API.Registers
{
    public static partial class Register
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration config)
        {
            ConnectionStringSettings configurations = new(config);

            services.RegisterIdentity(configurations);

            return services;
        }

        public static IServiceCollection RegisterIdentity(this IServiceCollection services, ConnectionStringSettings configurations)
        {
            services.AddScoped<IAuthenticationIdentity, AuthenticationIdentity>()
            .AddDbContext<AuthenticationIdentity>(options =>
            {
                options.UseSqlServer(configurations.AuthenticationIdentityDbConnectionString, sqlServerOptionsAction: sqlOptions =>
                {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(15),
                            errorNumbersToAdd: null);
                });
            });
            services.AddDataProtection();
            services.AddIdentityCore<Microsoft.AspNetCore.Identity.IdentityUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
            .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationIdentity>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}