

namespace Authentication.Core.Persistence.Admin.Repo
{

    using Authentication.Core.Persistence.Database.Context;
    using Microsoft.EntityFrameworkCore;
    using Authentication.Core.Persistence.Users.Entity;
    using Authentication.Common.Result;
    using Authentication.Core.Domain.Users;
    using Authentication.Core.Persistence.Users.Factory;

    public class AdminRepo : IAdminRepo
    {
        public readonly IAuthenticationIdentity _authenticationIdentity;

        public readonly DbSet<Users> _dbSet;

        public AdminRepo(IAuthenticationIdentity authenticationIdentity)
        {
            _authenticationIdentity = authenticationIdentity;
            _dbSet = _authenticationIdentity.Set<Users>();
        }

        public async Task<Result<Admin>> AddAdminAsync(Admin admin)
        {
            await _dbSet.AddAsync(admin.ToEntity());

            return Result.Ok(admin);
        }

    }
}