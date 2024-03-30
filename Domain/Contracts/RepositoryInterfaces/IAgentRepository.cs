using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IAgentRepository : IRepositoryContract<Agent>
    {
        Task<IEnumerable<Agent>> GetAllAsync(CancellationToken cancellationToken);
        Task<Agent> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Agent> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(Agent Agent);
        void UpdateDetail(Agent Agent);
        void DeleteDetail(Agent Agent);
    }
}