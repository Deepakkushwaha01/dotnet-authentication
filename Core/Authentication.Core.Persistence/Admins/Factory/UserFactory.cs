namespace Authentication.Core.Persistence.Users.Factory
{
    using Authentication.Core.Persistence.Users.Entity;
    using Authentication.Core.Domain.Users;
    public static class UsersFactory
    {
        // ---------------- Domain → Entity ----------------
        public static Users ToEntity(this User domain)
        {
            return Users.Create(
                domain.Id,
                domain.UserName,
                domain.NormalizedUserName,
                domain.Email,
                domain.NormalizedEmail,
                domain.EmailConfirmed,
                domain.PasswordHash,
                domain.SecurityStamp ?? Guid.NewGuid().ToString(),
                domain.ConcurrencyStamp ?? Guid.NewGuid().ToString(),
                domain.FirstName,
                domain.LastName,
                domain.Uid != Guid.Empty ? domain.Uid : Guid.NewGuid(),
                domain.CreatedOn != default ? domain.CreatedOn : DateTime.UtcNow,
                domain.DeletedOn,
                domain.AccessFailedCount,
                domain.LockoutEnabled,
                domain.PhoneNumberConfirmed,
                domain.TwoFactorEnabled
            );
        }

        // ---------------- Entity → Domain ----------------

public static User ToDomain(this Users entity)
{
    return User.Create(
        userName: entity.UserName,                // userName
        email: entity.Email,                   // email
        firstName: entity.FirstName,               // firstName
        lastName: entity.LastName,                // lastName
        passwordHash: entity.PasswordHash,             // passwordHash
        id: entity.Id,
        normalizedUserName: entity.NormalizedUserName ?? string.Empty,
        normalizedEmail: entity.NormalizedEmail,
        emailConfirmed: entity.EmailConfirmed,
        securityStamp: entity.SecurityStamp,
        concurrencyStamp: entity.ConcurrencyStamp,
        uid: entity.Uid,
        createdOn: entity.CreatedOn,
        deletedOn: entity.DeletedOn,
        accessFailedCount: entity.AccessFailedCount,
        lockoutEnabled: entity.LockoutEnabled,
        phoneNumberConfirmed: entity.PhoneNumberConfirmed,
        twoFactorEnabled: entity.TwoFactorEnabled
    );
}


    }
}
