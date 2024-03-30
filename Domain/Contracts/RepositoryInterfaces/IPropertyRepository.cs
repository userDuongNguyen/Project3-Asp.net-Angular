using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IPropertyRepository : IRepositoryContract<Property>
    {
        Task<IEnumerable<Property>> GetAllAsync(CancellationToken cancellationToken);
        Task<Property> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Property> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(Property Property);
        void UpdateDetail(Property Property);
        void DeleteDetail(Property Property);
    }
}