namespace Authentication.Core.Persistence.Database.Context
{
    using Authentication.Core.Persistence.Database.Mappings;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Authentication.Core.Persistence.Admins.Entity;
    
    public class AuthenticationIdentity : IdentityDbContext<AdminEntity, IdentityRole, string>, IAuthenticationIdentity
    {
        public AuthenticationIdentity(DbContextOptions<AuthenticationIdentity> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AdminMappings());

            builder.Entity<IdentityRole>(b => { b.ToTable("Roles"); });
            builder.Entity<IdentityUserRole<string>>(b => { b.ToTable("IdentityRoles"); });
            builder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("IdentityClaims"); });
            builder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("IdentityLogins"); });
            builder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("RoleClaims"); });
            builder.Entity<IdentityUserToken<string>>(b => { b.ToTable("IdentityTokens"); });
        }

        public void HealthCheck()
        {
            Database.OpenConnection();
            Database.CloseConnection();
        }
    }
}
