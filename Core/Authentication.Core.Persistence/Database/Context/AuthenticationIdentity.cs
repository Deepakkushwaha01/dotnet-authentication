namespace Authentication.Core.Persistence.Database.Context
{
    using Authentication.Core.Persistence.Database.Mappings;
    using Authentication.Core.Persistence.Identity.Entity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AuthenticationIdentity : IdentityDbContext<IdentityEntity, IdentityRole<long>, long>, IAuthenticationIdentity
    {
        public AuthenticationIdentity(DbContextOptions<AuthenticationIdentity> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new IdentityMappings());

            builder.Entity<IdentityRole>(b => { b.ToTable("Roles"); });
            builder.Entity<IdentityUserRole<long>>(b => { b.ToTable("IdentityRoles"); });
            builder.Entity<IdentityUserClaim<long>>(b => { b.ToTable("IdentityClaims"); });
            builder.Entity<IdentityUserLogin<long>>(b => { b.ToTable("IdentityLogins"); });
            builder.Entity<IdentityRoleClaim<long>>(b => { b.ToTable("RoleClaims"); });
            builder.Entity<IdentityUserToken<long>>(b => { b.ToTable("IdentityTokens"); });

            // Seed data
            builder.Entity<IdentityRole<long>>().HasData(
            new IdentityRole<long> { Id = 1, Name = "SuperAdmin", NormalizedName = "SUPERADMIN", ConcurrencyStamp = "81b05e6a-f421-4f3a-ad26-d80bdf9d4761" },
            new IdentityRole<long> { Id = 2, Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = "af086f29-8bec-41b8-a6dc-62c518dffc9f" },
            new IdentityRole<long> { Id = 3, Name = "User", NormalizedName = "USER", ConcurrencyStamp = "3a9807b7-b708-4bb2-a08d-7418e31c4c46" }
        );

        }

        public void HealthCheck()
        {
            Database.OpenConnection();
            Database.CloseConnection();
        }
    }
}
