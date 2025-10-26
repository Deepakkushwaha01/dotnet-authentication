namespace Authentication.Core.Persistence.Users.Factory
{
    using System;
    using Authentication.Core.Domain.Users;
    using Authentication.Core.Persistence.Identity.Entity;

    public static class AdminsFactory
    {
        // ---------------- Domain → Entity ----------------
        public static IdentityEntity ToEntity(this IdentityDomain domain)
        {
            return IdentityEntity.Create(
                id: domain.Id,
                email: domain.Email,
                normalizedEmail: domain.NormalizedEmail,
                emailConfirmed: domain.EmailConfirmed,
                passwordHash: domain.PasswordHash,
                phoneNumber: domain.PhoneNumber,
                securityStamp: domain.SecurityStamp ?? Guid.NewGuid().ToString(),
                concurrencyStamp: domain.ConcurrencyStamp ?? Guid.NewGuid().ToString(),
                firstName: domain.FirstName,
                lastName: domain.LastName,
                uid: domain.Uid == Guid.Empty ? Guid.NewGuid() : domain.Uid,
                createdOn: domain.CreatedOn == default ? DateTime.UtcNow : domain.CreatedOn,
                accessFailedCount: domain.AccessFailedCount,
                lockoutEnabled: domain.LockoutEnabled,
                
                phoneNumberConfirmed: domain.PhoneNumberConfirmed,
                twoFactorEnabled: domain.TwoFactorEnabled,
                isVerified: domain.IsVerified,
                businessAddress: domain.BusinessAddress ?? string.Empty,
                updatedOn: domain.UpdatedOn
            );
        }

        // ---------------- Entity → Domain ----------------
        public static IdentityDomain ToDomain(this IdentityEntity entity)
        {
            return IdentityDomain.Create(
                id: entity.Id,
                email: entity.Email,
                normalizedEmail: entity.NormalizedEmail,
                emailConfirmed: entity.EmailConfirmed,
                passwordHash: entity.PasswordHash,
                securityStamp: entity.SecurityStamp,
                concurrencyStamp: entity.ConcurrencyStamp,
                phoneNumber: entity.PhoneNumber,
                firstName: entity.FirstName,
                lastName: entity.LastName,
                uid: entity.Uid,
                createdOn: entity.CreatedOn,
                accessFailedCount: entity.AccessFailedCount,
                lockoutEnabled: entity.LockoutEnabled,
                phoneNumberConfirmed: entity.PhoneNumberConfirmed,
                twoFactorEnabled: entity.TwoFactorEnabled,
                UserRoles: entity.UserRoles?.Select(ur => ur.ToDomain()).ToList() ?? new List<IdentityUserRoleDomain>()

            );
        }
    }
}
