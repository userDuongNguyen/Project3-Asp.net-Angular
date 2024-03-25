using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IAgentRepository : IRepositoryContract<Agent>
    {
        Task<IEnumerable<Agent>> GetAllAgentsAsync(CancellationToken cancellationToken = default);
        Task<Agent> GetAgentByIdAsync(Guid AgentId, CancellationToken cancellationToken = default);
        Task<Agent> GetAgentWithDetailsAsync(Guid AgentId);
        void CreateAgent(Agent Agent);
        void UpdateAgent(Agent Agent);
        void DeleteAgent(Agent Agent);
        void Save();
    }
}
