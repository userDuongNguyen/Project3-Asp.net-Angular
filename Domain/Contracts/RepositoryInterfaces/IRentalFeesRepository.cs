using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IRentalFeeRepository : IRepositoryContract<RentalFee>
    {
        Task<IEnumerable<RentalFee>> GetAllAsync(CancellationToken cancellationToken);
        Task<RentalFee> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<RentalFee> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(RentalFee RentalFee);
        void UpdateDetail(RentalFee RentalFee);
        void DeleteDetail(RentalFee RentalFee);
    }
}