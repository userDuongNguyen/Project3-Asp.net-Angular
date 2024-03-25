using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IWalletRepository : IRepositoryContract<Wallet>
    {
        Task<IEnumerable<Wallet>> GetAllWalletsAsync(CancellationToken cancellationToken = default);
        Task<Wallet> GetWalletByIdAsync(Guid WalletId, CancellationToken cancellationToken = default);
        Task<Wallet> GetWalletWithDetailsAsync(Guid WalletId);
        void CreateWallet(Wallet Wallet);
        void UpdateWallet(Wallet Wallet);
        void DeleteWallet(Wallet Wallet);
        void Save();
    }
}
