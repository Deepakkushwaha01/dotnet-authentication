

namespace Authentication.Core.Persistence.IdentityRepo.Repo
{
    using Authentication.Common.Result;
    using Authentication.Core.Domain.Users;
    using Authentication.Core.Persistence.Identity.Entity;
    using Authentication.Core.Persistence.Users.Factory;
    using Microsoft.AspNetCore.Identity;

    public class IdentityRepo : IIdentityRepo
    {
        public readonly UserManager<IdentityEntity> _userManager;
    

        public IdentityRepo(UserManager<IdentityEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<IdentityDomain>> AddIdentityAsync(IdentityDomain identity)
        {
            var identityEntity = identity.ToEntity();
            var result = await _userManager.CreateAsync(identityEntity, identityEntity.PasswordHash);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result.Invalid<IdentityDomain>($"Failed to create identity: {errors}");
            }

            await _userManager.AddToRoleAsync(identityEntity, "User"); 

            return Result.Ok(identity);
        }

    }
}