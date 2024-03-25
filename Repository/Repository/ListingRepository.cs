using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class ListingRepository(RepositoryContext repositoryContext) : RepositoryBase<Listing>(repositoryContext), IListingRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Listing>> GetAllListingsAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.Headline)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Listing> GetListingByIdAsync(Guid ListingId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Listing => Listing.Id.Equals(ListingId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Listing> GetListingWithDetailsAsync(Guid ListingId)
        {
            return await FindByCondition(
                Listing => Listing.Id.Equals(ListingId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateListing(Listing Listing)
        {
            Create(Listing);
        }

        public void UpdateListing(Listing Listing)
        {
            Update(Listing);
        }

        public void DeleteListing(Listing Listing)
        {
            Delete(Listing);
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

