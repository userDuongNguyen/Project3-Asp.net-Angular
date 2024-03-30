using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IAddressRepository : IRepositoryContract<Address>
    {
        Task<IEnumerable<Address>> GetAllAsync(CancellationToken cancellationToken);
        Task<Address> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Address> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(Address Address);
        void UpdateDetail(Address Address);
        void DeleteDetail(Address Address);
    }
}