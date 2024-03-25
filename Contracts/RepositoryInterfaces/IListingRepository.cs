using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IListingRepository : IRepositoryContract<Listing>
    {
        Task<IEnumerable<Listing>> GetAllListingsAsync(CancellationToken cancellationToken = default);
        Task<Listing> GetListingByIdAsync(Guid ListingId, CancellationToken cancellationToken = default);
        Task<Listing> GetListingWithDetailsAsync(Guid ListingId);
        void CreateListing(Listing Listing);
        void UpdateListing(Listing Listing);
        void DeleteListing(Listing Listing);
        void Save();
    }
}
