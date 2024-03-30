using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IAccommodationDetailRepository : IRepositoryContract<AccommodationDetail>
    {
        Task<IEnumerable<AccommodationDetail>> GetAllAsync(CancellationToken cancellationToken);
        Task<AccommodationDetail> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<AccommodationDetail> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(AccommodationDetail AccommodationDetail);
        void UpdateDetail(AccommodationDetail AccommodationDetail);
        void DeleteDetail(AccommodationDetail AccommodationDetail);
    }
}