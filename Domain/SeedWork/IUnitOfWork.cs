namespace Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task<int> CommitAsync();
        void CommitAndRefreshChanges();
        void RollbackChanges();
    }
}
