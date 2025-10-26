using Authentication.Core.Persistence.Admins.Entity;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Core.Persistence.Identity.Entity
{
    public class IdentityEntity : IdentityUser<long>
    {
        // ---------------- Custom Fields ----------------
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Guid Uid { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public bool IsVerified { get; private set; } = false;
        public string BusinessAddress { get; private set; } = string.Empty;

        public List<IdentityUserRoleEntity> UserRoles { get; private set; } = new List<IdentityUserRoleEntity>();

        // EF Core ke liye private constructor
        private IdentityEntity() { }

        // ---------------- Factory Method ----------------
        public static IdentityEntity Create(
            long id,
            string email,
            string normalizedEmail,
            bool emailConfirmed,
            string passwordHash,
            string securityStamp,
            string concurrencyStamp,
            string phoneNumber,
            string firstName,
            string lastName,
            Guid uid,
            DateTime createdOn,
            int accessFailedCount,
            bool lockoutEnabled,
            bool phoneNumberConfirmed,
            bool twoFactorEnabled,
            bool isVerified,
            string businessAddress,
            DateTime? updatedOn = null)
        {
            return new IdentityEntity
            {
                Id = id,
                Email = email,
                NormalizedEmail = normalizedEmail,
                UserName = email,
                NormalizedUserName = normalizedEmail,
                EmailConfirmed = emailConfirmed,
                PasswordHash = passwordHash,
                SecurityStamp = securityStamp,
                ConcurrencyStamp = concurrencyStamp,
                FirstName = firstName,
                LastName = lastName,
                Uid = uid,
                CreatedOn = createdOn,
                AccessFailedCount = accessFailedCount,
                LockoutEnabled = lockoutEnabled,
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = phoneNumberConfirmed,
                TwoFactorEnabled = twoFactorEnabled,
                IsVerified = isVerified,
                BusinessAddress = businessAddress ?? string.Empty,
                UpdatedOn = updatedOn
            };
        }
    }
}
