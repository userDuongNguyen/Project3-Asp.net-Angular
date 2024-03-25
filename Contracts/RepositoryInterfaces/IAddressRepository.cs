using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IAddressRepository : IRepositoryContract<Address>
    {
        Task<IEnumerable<Address>> GetAllAddressAsync(CancellationToken cancellationToken = default);
        Task<Address> GetAddressByIdAsync(Guid AddressId, CancellationToken cancellationToken = default);
        Task<Address> GetAddressWithDetailsAsync(Guid AddressId);
        void CreateAddress(Address Address);
        void UpdateAddress(Address Address);
        void DeleteAddress(Address Address);
        void Save();
    }
}
