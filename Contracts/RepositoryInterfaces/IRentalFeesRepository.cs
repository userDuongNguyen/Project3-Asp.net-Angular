using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IRentalFeesRepository : IRepositoryContract<RentalFees>
    {
        Task<IEnumerable<RentalFees>> GetAllRentalFeessAsync(CancellationToken cancellationToken = default);
        Task<RentalFees> GetRentalFeesByIdAsync(Guid RentalFeesId, CancellationToken cancellationToken = default);
        Task<RentalFees> GetRentalFeesWithDetailsAsync(Guid RentalFeesId);
        void CreateRentalFees(RentalFees RentalFees);
        void UpdateRentalFees(RentalFees RentalFees);
        void DeleteRentalFees(RentalFees RentalFees);
        void Save();
    }
}
