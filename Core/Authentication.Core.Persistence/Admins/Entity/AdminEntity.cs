using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Core.Persistence.Admins.Entity
{
    public class AdminEntity : IdentityUser
    {
        // ---------------- Custom Fields ----------------
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Guid Uid { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }

        public byte AdminLevel { get; private set; }
        public bool IsVerified { get; private set; } = false;
        public string BusinessAddress { get; private set; } = string.Empty;

        // EF Core ke liye private constructor
        private AdminEntity() { }

        // ---------------- Factory Method ----------------
        public static AdminEntity Create(
            string id,
            string email,
            string normalizedEmail,
            bool emailConfirmed,
            string passwordHash,
            string securityStamp,
            string concurrencyStamp,
            string firstName,
            string lastName,
            Guid uid,
            DateTime createdOn,
            int accessFailedCount,
            bool lockoutEnabled,
            bool phoneNumberConfirmed,
            bool twoFactorEnabled,
            byte adminLevel,
            bool isVerified,
            string businessAddress,
            DateTime? updatedOn = null)
        {
            return new AdminEntity
            {
                Id = id,
                Email = email,
                NormalizedEmail = normalizedEmail,
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
                PhoneNumberConfirmed = phoneNumberConfirmed,
                TwoFactorEnabled = twoFactorEnabled,
                AdminLevel = adminLevel,
                IsVerified = isVerified,
                BusinessAddress = businessAddress ?? string.Empty,
                UpdatedOn = updatedOn
            };
        }
    }
}
