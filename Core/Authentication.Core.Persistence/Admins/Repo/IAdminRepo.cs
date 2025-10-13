namespace Authentication.Core.Persistence.Admin.Repo
{
    using Authentication.Common.Result;
    using Authentication.Core.Domain.Users;

    public interface IAdminRepo
    {
        Task<Result<AdminDomain>> AddAdminAsync(AdminDomain user);
    }
}
