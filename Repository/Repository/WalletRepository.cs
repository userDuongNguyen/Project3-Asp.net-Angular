using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class WalletRepository(RepositoryContext repositoryContext) : RepositoryBase<Wallet>(repositoryContext), IWalletRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync(CancellationToken cancellationToken = default) => await FindAll()
               .OrderBy(ad => ad.Id)
               .ToListAsync(cancellationToken: cancellationToken);

        public async Task<Wallet> GetWalletByIdAsync(Guid WalletId, CancellationToken cancellationToken = default)
            => await FindByCondition(
                        Wallet => Wallet.Id.Equals(WalletId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        public async Task<Wallet> GetWalletWithDetailsAsync(Guid WalletId)
        {
            return await FindByCondition(
                Wallet => Wallet.Id.Equals(WalletId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateWallet(Wallet Wallet)
        {
            Create(Wallet);
        }

        public void UpdateWallet(Wallet Wallet)
        {
            Update(Wallet);
        }

        public void DeleteWallet(Wallet Wallet)
        {
            Delete(Wallet);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}



