using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IPropertyRepository : IRepositoryContract<Property>
    {
        Task<IEnumerable<Property>> GetAllPropertysAsync(CancellationToken cancellationToken = default);
        Task<Property> GetPropertyByIdAsync(Guid PropertyId, CancellationToken cancellationToken = default);
        Task<Property> GetPropertyWithDetailsAsync(Guid PropertyId);
        void CreateProperty(Property Property);
        void UpdateProperty(Property Property);
        void DeleteProperty(Property Property);
        void Save();
    }
}
