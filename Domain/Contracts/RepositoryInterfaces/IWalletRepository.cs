using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IWalletRepository : IRepositoryContract<Wallet>
    {
        Task<IEnumerable<Wallet>> GetAllAsync(CancellationToken cancellationToken);
        Task<Wallet> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Wallet> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(Wallet Wallet);
        void UpdateDetail(Wallet Wallet);
        void DeleteDetail(Wallet Wallet);
    }
}