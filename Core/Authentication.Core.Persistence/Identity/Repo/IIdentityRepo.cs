namespace Authentication.Core.Persistence.IdentityRepo.Repo
{
    using Authentication.API.DTOs;
    using Authentication.Common.Result;
    using Authentication.Core.Domain.Users;

    public interface IIdentityRepo
    {
        Task<Result<IdentityResponseDto>> AddIdentityAsync(IdentityDomain user);
        Task<Result<IdentityDomain>> GetUserByEmailOrUid(string email, Guid? uid, List<string> includeRelations = null);

        Task<Result<bool>> CheckPassword(IdentityDomain user, string password);
    }
}
