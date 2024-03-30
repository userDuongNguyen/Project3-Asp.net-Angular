using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class WalletRepository
        (DataContext repositoryContext) : RepositoryBase<Wallet>(repositoryContext),
    IWalletRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Wallet>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Wallet> GetByIdAsync(int WalletId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Wallet => Wallet.Id.Equals(WalletId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Wallet> GetWithDetailsAsync(int WalletId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                Wallet => Wallet.Id.Equals(WalletId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(Wallet Wallet)
        {
            Create(Wallet);
        }

        public void UpdateDetail(Wallet Wallet)
        {
            Update(Wallet);
        }

        public void DeleteDetail(Wallet Wallet)
        {
            Delete(Wallet);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
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

