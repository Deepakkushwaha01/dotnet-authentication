namespace Authentication.Core.Persistence.Admin.Repo
{
    using Authentication.Common.Result;
    using Authentication.Core.Domain.Users;

    public interface IAdminRepo
    {
        Task<Result<User>> AddAdminAsync(User user);
    }
}
