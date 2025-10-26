namespace Authentication.Core.Persistence.IdentityRepo.Repo
{
    using Authentication.API.DTOs;
    using Authentication.Common.Result;
    using Authentication.Core.Domain.Users;
    using Authentication.Core.Persistence.Admins.Entity;
    using Authentication.Core.Persistence.Database.Context;
    using Authentication.Core.Persistence.Identity.Entity;
    using Authentication.Core.Persistence.Users.Factory;
    using Authentication.Core.SharedKernel.Enums;
    using Authentication.Core.SharedKernel.Enums.IdentityRoleEnum;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class IdentityRepo : IIdentityRepo
    {
        private readonly IAuthenticationIdentity _dbContext;

        public IdentityRepo(IAuthenticationIdentity dbContext)
        {
            _dbContext = dbContext;
        }

        // ---------------- Create or Add Role to Existing User ----------------
        public async Task<Result<IdentityResponseDto>> AddIdentityAsync(IdentityDomain identity)
        {
            // Check if user exists with email
            var validUser = await _dbContext.Set<IdentityEntity>()
                .FirstOrDefaultAsync(u => u.Email == identity.Email);

            if (validUser != null)
            {
                var requestedRole = (IdentityRoleEnum)identity.AdminLevel;
                var roleName = requestedRole.ToString();

                var userRoles = await _dbContext.Set<IdentityUserRoleEntity>()
                    .Where(r => r.UserId == validUser.Id)
                    .Join(
                        _dbContext.Set<IdentityRole<long>>(),
                        ur => ur.RoleId,
                        r => r.Id,
                        (ur, r) => r.Name
                    )
                    .ToListAsync();

                if (!userRoles.Contains(roleName))
                {
                    // Add new role mapping
                    var roleEntity = await _dbContext.Set<IdentityRole<long>>()
                        .FirstOrDefaultAsync(r => r.Name == roleName);

                    if (roleEntity == null)
                        return Result.Invalid<IdentityResponseDto>($"Role {roleName} does not exist.");

                    var userRole = new IdentityUserRoleEntity
                    {
                        UserId = validUser.Id,
                        RoleId = roleEntity.Id,
                        AssignedBy = "System",
                        AssignedOn = DateTime.UtcNow,
                        IsActive = true
                    };

                    await _dbContext.Set<IdentityUserRoleEntity>().AddAsync(userRole);
                    await _dbContext.SaveChangesAsync();

                    return Result.Ok(new IdentityResponseDto
                    {
                        Uid = validUser.Uid,
                        Email = validUser.Email,
                        FirstName = validUser.FirstName,
                        LastName = validUser.LastName
                    });
                }

                return Result.Invalid<IdentityResponseDto>($"User with email {identity.Email} already has this role.");
            }

            // Create new user
            var entity = identity.ToEntity();

            await _dbContext.Set<IdentityEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            // Assign default role = User
            var defaultRole = await _dbContext.Set<IdentityRole<long>>()
                .FirstOrDefaultAsync(r => r.Name == IdentityRoleEnum.User.ToString());

            if (defaultRole != null)
            {
                await _dbContext.Set<IdentityUserRoleEntity>().AddAsync(new IdentityUserRoleEntity
                {
                    UserId = entity.Id,
                    RoleId = defaultRole.Id,
                    AssignedOn = DateTime.UtcNow,
                    AssignedBy = "System",
                    IsActive = true
                });
                await _dbContext.SaveChangesAsync();
            }

            return Result.Ok(new IdentityResponseDto
            {
                Uid = entity.Uid,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            });
        }

        // ---------------- Get User by Email or UID with optional relations ----------------
        public async Task<Result<IdentityDomain>> GetUserByEmailOrUid(string email, Guid? uid, List<string> includeRelations = null)
        {
            IQueryable<IdentityEntity> query = _dbContext.Set<IdentityEntity>();

            if (includeRelations != null && includeRelations.Any())
            {
                foreach (var relation in includeRelations)
                {
                    if(relation == IdentityTable.IdentityUserRoles.ToString())
                    query = query.Include(x => x.UserRoles);
                }
            }

            if (string.IsNullOrEmpty(email) && uid == null)
                return Result.Invalid<IdentityDomain>("Either email or UID must be provided.");

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email == email);
            else
                query = query.Where(u => u.Uid == uid);

            var user = await query.FirstOrDefaultAsync();
            if (user == null)
                return Result.NotFound<IdentityDomain>($"User with email {email ?? uid.ToString()} not found.");

            return Result.Ok(user.ToDomain());
        }

        // ---------------- Password Check without UserManager ----------------
        public Task<Result<bool>> CheckPassword(IdentityDomain userDomain, string password)
        {
            var entity = userDomain.ToEntity();
            var passwordHasher = new PasswordHasher<IdentityEntity>();
            var result = passwordHasher.VerifyHashedPassword(entity, entity.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
                return Task.FromResult(Result.Unauthorized<bool>("Invalid password."));

            return Task.FromResult(Result.Ok(true));
        }
    }
}
