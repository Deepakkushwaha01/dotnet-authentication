namespace Authentication.Core.Persistence.Database.Context
{
    using Authentication.Core.Persistence.Admins.Entity;
    using Authentication.Core.Persistence.Database.Mappings;
    using Authentication.Core.Persistence.Identity.Entity;
    using Authentication.Core.SharedKernel.Enums;
    using Authentication.Core.SharedKernel.Enums.IdentityRoleEnum;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AuthenticationIdentity : IdentityDbContext<
    IdentityEntity,
    IdentityRole<long>,
    long,
    IdentityUserClaim<long>,
    IdentityUserRoleEntity,
    IdentityUserLogin<long>,
    IdentityRoleClaim<long>,
    IdentityToken>, IAuthenticationIdentity
    {
        public AuthenticationIdentity(DbContextOptions<AuthenticationIdentity> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new IdentityMappings());
            builder.ApplyConfiguration(new IdentityUserRoleMappings());

            builder.Entity<IdentityRole<long>>(b => { b.ToTable(IdentityTable.Roles.ToString()); });
            builder.Entity<IdentityUserClaim<long>>(b => { b.ToTable(IdentityTable.IdentityClaims.ToString()); });
            builder.Entity<IdentityUserLogin<long>>(b => { b.ToTable(IdentityTable.IdentityLogins.ToString()); });
            builder.Entity<IdentityRoleClaim<long>>(b => { b.ToTable(IdentityTable.RoleClaims.ToString()); });
            builder.Entity<IdentityToken>(b => { b.ToTable(IdentityTable.IdentityTokens.ToString()); });


            // Seed data
            builder.Entity<IdentityRole<long>>().HasData(
            new IdentityRole<long> { Id = 1, Name = IdentityRoleEnum.SuperAdmin.ToString(), NormalizedName = IdentityRoleEnum.SuperAdmin.ToString().ToUpper(), ConcurrencyStamp = "81b05e6a-f421-4f3a-ad26-d80bdf9d4761" },
            new IdentityRole<long> { Id = 2, Name = IdentityRoleEnum.Manager.ToString(), NormalizedName = IdentityRoleEnum.Manager.ToString().ToUpper(), ConcurrencyStamp = "af086f29-8bec-41b8-a6dc-62c518dffc9f" },
            new IdentityRole<long> { Id = 3, Name = IdentityRoleEnum.User.ToString(), NormalizedName = IdentityRoleEnum.User.ToString().ToUpper(), ConcurrencyStamp = "3a9807b7-b708-4bb2-a08d-7418e31c4c46" },
            new IdentityRole<long> { Id = 4, Name = IdentityRoleEnum.Admin.ToString(), NormalizedName = IdentityRoleEnum.Admin.ToString().ToUpper(), ConcurrencyStamp = "0bbcf321-b5d5-44bb-a2f9-2f4df0b68254" }
        );

        }

        public void HealthCheck()
        {
            Database.OpenConnection();
            Database.CloseConnection();
        }
    }
}
