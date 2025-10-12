namespace Authentication.Core.Domain.Users
{
    public class Admin
    {
        // ---------------- Core Identity Fields ----------------
        public string Id { get; private set; }
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

        // ---------------- Factory Method ----------------
        public static Admin Create(
            string email,
            string firstName,
            string lastName,
            string passwordHash,
            string phoneNumber = null,
            byte adminLevel = 1,
            bool isVerified = false,
            string businessAddress = "")
        {
            var admin = new Admin
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
                TwoFactorEnabled = false
            };

            return admin;
        }

        public static Admin Create(
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
            bool twoFactorEnabled)
        {
            return new Admin
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
                TwoFactorEnabled = twoFactorEnabled
            };
        }

        // ---------------- Methods to modify state ----------------
        public void MarkEmailConfirmed() => EmailConfirmed = true;

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("First and last name are required");

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
