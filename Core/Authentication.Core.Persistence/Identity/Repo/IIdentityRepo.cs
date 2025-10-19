namespace Authentication.Core.Persistence.IdentityRepo.Repo
{
    using Authentication.Common.Result;
    using Authentication.Core.Domain.Users;

    public interface IIdentityRepo
    {
        Task<Result<IdentityDomain>> AddIdentityAsync(IdentityDomain user);
    }
}
