

namespace Authentication.Core.Persistence.Admin.Repo
{

    using Authentication.Core.Persistence.Database.Context;
    using Microsoft.EntityFrameworkCore;
    using Authentication.Common.Result;
    using Authentication.Core.Persistence.Users.Factory;
    using Authentication.Core.Persistence.Admins.Entity;
    using Authentication.Core.Domain.Users;

    public class AdminRepo : IAdminRepo
    {
        public readonly IAuthenticationIdentity _authenticationIdentity;

        public readonly DbSet<AdminEntity> _dbSet;

        public AdminRepo(IAuthenticationIdentity authenticationIdentity)
        {
            _authenticationIdentity = authenticationIdentity;
            _dbSet = _authenticationIdentity.Set<AdminEntity>();
        }

        public async Task<Result<AdminDomain>> AddAdminAsync(AdminDomain admin)
        {
            await _dbSet.AddAsync(admin.ToEntity());

            return Result.Ok(admin);
        }

    }
}