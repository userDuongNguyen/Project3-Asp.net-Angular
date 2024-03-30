using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ListingRepository
        (DataContext repositoryContext) : RepositoryBase<Listing>(repositoryContext),
    IListingRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Listing>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Listing> GetByIdAsync(int ListingId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Listing => Listing.Id.Equals(ListingId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Listing> GetWithDetailsAsync(int ListingId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                Listing => Listing.Id.Equals(ListingId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(Listing Listing)
        {
            Create(Listing);
        }

        public void UpdateDetail(Listing Listing)
        {
            Update(Listing);
        }

        public void DeleteDetail(Listing Listing)
        {
            Delete(Listing);
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

