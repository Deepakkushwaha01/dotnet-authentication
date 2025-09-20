using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Core.Persistence.Database.Context
{
    public class AuthenticationIdentity : IdentityDbContext
    {
        public AuthenticationIdentity(DbContextOptions<AuthenticationIdentity> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(b => { b.ToTable("Users"); });
            builder.Entity<IdentityRole>(b => { b.ToTable("Roles"); });
            builder.Entity<IdentityUserRole<string>>(b => { b.ToTable("UserRoles"); });
            builder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("UserLogins"); });
            builder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("RoleClaims"); });
            builder.Entity<IdentityUserToken<string>>(b => { b.ToTable("UserTokens"); });
        }
    }
}
