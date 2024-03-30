using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IListingRepository : IRepositoryContract<Listing>
    {
        Task<IEnumerable<Listing>> GetAllAsync(CancellationToken cancellationToken);
        Task<Listing> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Listing> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(Listing Listing);
        void UpdateDetail(Listing Listing);
        void DeleteDetail(Listing Listing);
    }
}