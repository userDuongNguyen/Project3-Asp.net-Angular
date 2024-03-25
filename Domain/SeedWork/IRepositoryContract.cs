namespace Domain.SeedWork
{
    public interface IRepositoryContract<in T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }

    }
}
