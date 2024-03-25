using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface ISubcriptionRepository : IRepositoryContract<Subcription>
    {
        Task<IEnumerable<Subcription>> GetAllSubcriptionsAsync(CancellationToken cancellationToken = default);
        Task<Subcription> GetSubcriptionByIdAsync(Guid SubcriptionId, CancellationToken cancellationToken = default);
        Task<Subcription> GetSubcriptionWithDetailsAsync(Guid SubcriptionId);
        void CreateSubcription(Subcription Subcription);
        void UpdateSubcription(Subcription Subcription);
        void DeleteSubcription(Subcription Subcription);
        void Save();
    }
}
