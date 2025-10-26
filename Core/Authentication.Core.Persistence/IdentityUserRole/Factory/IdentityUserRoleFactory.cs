namespace Authentication.Core.Persistence.Users.Factory
{
    using System;
    using Authentication.Core.Domain.Users;
    using Authentication.Core.Persistence.Admins.Entity;

    public static class UserRoleFactory
    {
        // ---------------- Domain → Entity ----------------
        public static IdentityUserRoleEntity ToEntity(this IdentityUserRoleDomain domain)
        {
            return new IdentityUserRoleEntity
            {
                UserId = domain.UserId,
                RoleId = domain.RoleId,
                AssignedOn = domain.AssignedOn,
                AssignedBy = domain.AssignedBy ?? string.Empty,
                IsActive = domain.IsActive
            };
        }

        // ---------------- Entity → Domain ----------------
        public static IdentityUserRoleDomain ToDomain(this IdentityUserRoleEntity entity)
        {
            // Use private constructor + factory pattern
            return IdentityUserRoleDomain.Create(
                userId: entity.UserId,
                roleId: entity.RoleId,
                assignedBy: entity.AssignedBy ?? string.Empty
            ).AlsoSetAdditionalProperties(entity);
        }

        // ---------------- Helper: set additional properties not set by Create ----------------
        private static IdentityUserRoleDomain AlsoSetAdditionalProperties(
            this IdentityUserRoleDomain domain,
            IdentityUserRoleEntity entity)
        {
            // Override assignedOn and isActive if different from default
            if (entity.AssignedOn != default) domain.GetType()
                .GetProperty(nameof(IdentityUserRoleDomain.AssignedOn))
                ?.SetValue(domain, entity.AssignedOn);

            domain.GetType()
                .GetProperty(nameof(IdentityUserRoleDomain.IsActive))
                ?.SetValue(domain, entity.IsActive);

            return domain;
        }
    }
}
