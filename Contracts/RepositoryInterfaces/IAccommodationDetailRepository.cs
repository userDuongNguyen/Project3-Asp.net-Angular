using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IAccommodationDetailRepository : IRepositoryContract<AccommodationDetail>
    {
        Task<IEnumerable<AccommodationDetail>> GetAllAccommodationDetailsAsync(CancellationToken cancellationToken = default);
        Task<AccommodationDetail> GetAccommodationDetailByIdAsync(Guid AccommodationDetailId, CancellationToken cancellationToken = default);
        Task<AccommodationDetail> GetAccommodationDetailWithDetailsAsync(Guid AccommodationDetailId);
        void CreateAccommodationDetail(AccommodationDetail AccommodationDetail);
        void UpdateAccommodationDetail(AccommodationDetail AccommodationDetail);
        void DeleteAccommodationDetail(AccommodationDetail AccommodationDetail);
        void Save();
    }
}
