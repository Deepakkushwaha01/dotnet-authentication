namespace Authentication.Core.Domain.Users
{
    public class IdentityDomain
    {
        // ---------------- Core Identity Fields ----------------
        public long Id { get; private set; }
        public string Email { get; private set; }
        public string NormalizedEmail { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public string ConcurrencyStamp { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool PhoneNumberConfirmed { get; private set; }
        public bool TwoFactorEnabled { get; private set; }
        public DateTimeOffset? LockoutEnd { get; private set; }
        public bool LockoutEnabled { get; private set; }
        public int AccessFailedCount { get; private set; }

        // ---------------- Custom Fields ----------------
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid Uid { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public byte AdminLevel { get; private set; }

        public bool IsVerified { get; private set; } = false;

        public string BusinessAddress { get; private set; } = string.Empty;

        public DateTime? UpdatedOn { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        public List<IdentityUserRoleDomain> UserRoles { get; private set; } = new List<IdentityUserRoleDomain>();

        // ---------------- Factory Method ----------------
        public static IdentityDomain Create(
            string email,
            string firstName,
            string lastName,
            string passwordHash,
            string phoneNumber = null,
            byte adminLevel = 2,
            bool isVerified = false,
            string businessAddress = "",
            List<IdentityUserRoleDomain> UserRoles = null)
        {
            var identity = new IdentityDomain
            {
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = false,
                PhoneNumber = phoneNumber,
                PasswordHash = passwordHash,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,
                Uid = Guid.NewGuid(),
                AdminLevel = adminLevel,
                CreatedOn = DateTime.UtcNow,
                BusinessAddress = businessAddress,
                IsVerified = isVerified,
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                UserRoles = UserRoles ?? new List<IdentityUserRoleDomain>()
            };

            return identity;
        }

        public static IdentityDomain Create(
            long id,
            string email,
            string normalizedEmail,
            bool emailConfirmed,
            string passwordHash,
            string phoneNumber,
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
            List<IdentityUserRoleDomain> UserRoles = null)
        {
            return new IdentityDomain
            {
                Id = id,
                Email = email,
                NormalizedEmail = normalizedEmail,
                EmailConfirmed = emailConfirmed,
                PasswordHash = passwordHash,
                SecurityStamp = securityStamp,
                ConcurrencyStamp = concurrencyStamp,
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                Uid = uid,
                CreatedOn = createdOn,
                AccessFailedCount = accessFailedCount,
                LockoutEnabled = lockoutEnabled,
                PhoneNumberConfirmed = phoneNumberConfirmed,
                TwoFactorEnabled = twoFactorEnabled,
                UserRoles = UserRoles ?? new List<IdentityUserRoleDomain>()
            };
        }

        // ---------------- Methods to modify state ----------------
        public void MarkEmailConfirmed() => EmailConfirmed = true;

    }
}
