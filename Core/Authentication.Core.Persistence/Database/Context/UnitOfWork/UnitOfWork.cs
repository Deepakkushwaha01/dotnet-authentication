namespace Authentication.Core.Persistence.Database.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthenticationIdentity _dbContext;

        public UnitOfWork(AuthenticationIdentity dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }

}