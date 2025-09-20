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
            services.AddDbContext<AuthenticationIdentity>(options =>
                options.UseNpgsql(configurations.AuthenticationIdentityDbConnectionString));

            services.RegisterIdentity();

            return services;
        }

        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddIdentityCore<Microsoft.AspNetCore.Identity.IdentityUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationIdentity>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}