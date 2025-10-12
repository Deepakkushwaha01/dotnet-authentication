using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Core.Persistence.Users.Entity
{
    public class Users : IdentityUser
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Guid Uid { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        // EF Core ke liye private constructor
        private Users() { }

        // Domain → Entity mapping factory
        public static Users Create(
            string id,
            string userName,
            string normalizedUserName,
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
            DateTime? deletedOn,
            int accessFailedCount,
            bool lockoutEnabled,
            bool phoneNumberConfirmed,
            bool twoFactorEnabled)
        {
            return new Users
            {
                Id = id,
                UserName = userName,
                NormalizedUserName = normalizedUserName,
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
                DeletedOn = deletedOn,
                AccessFailedCount = accessFailedCount,
                LockoutEnabled = lockoutEnabled,
                PhoneNumberConfirmed = phoneNumberConfirmed,
                TwoFactorEnabled = twoFactorEnabled
            };
        }
    }
}
