using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IUserRepository : IRepositoryContract<User>
    {
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<User> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(User User);
        void UpdateDetail(User User);
        void DeleteDetail(User User);
    }
}