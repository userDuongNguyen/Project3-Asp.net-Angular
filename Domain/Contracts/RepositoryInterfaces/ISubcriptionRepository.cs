using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface ISubcriptionRepository : IRepositoryContract<Subcription>
    {
        Task<IEnumerable<Subcription>> GetAllAsync(CancellationToken cancellationToken);
        Task<Subcription> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Subcription> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(Subcription Subcription);
        void UpdateDetail(Subcription Subcription);
        void DeleteDetail(Subcription Subcription);
    }
}