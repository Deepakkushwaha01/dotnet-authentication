using Microsoft.AspNetCore.Identity;

namespace Authentication.Core.Persistence.Identity.Entity
{
    public class IdentityToken : IdentityUserToken<long>
    {
        // ---------------- Custom Fields ----------------
        public Guid TokenUid { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime ExpiresOn { get; private set; }

        // EF Core ke liye private constructor
        public IdentityToken() { }

        // ---------------- Factory Method ----------------
        public static IdentityToken Create(
            long userId,
            string loginProvider,
            string name,
            string value,
            Guid? tokenUid,
            double tokenExpiryDays,
            bool isRevoked)
        {
            return new IdentityToken
            {
                UserId = userId,
                LoginProvider = loginProvider,
                Name = name,
                Value = value,
                TokenUid = tokenUid ?? Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(tokenExpiryDays), // Example: Token valid for 7 days
            };
        }
    } 
}