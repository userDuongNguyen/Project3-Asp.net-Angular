using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IUserRepository : IRepositoryContract<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<User> GetUserByIdAsync(Guid UserId, CancellationToken cancellationToken = default);
        Task<User> GetUserWithDetailsAsync(Guid UserId);
        void CreateUser(User User);
        void UpdateUser(User User);
        void DeleteUser(User User);
        void Save();
    }
}
