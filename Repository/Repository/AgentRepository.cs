using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class AgentRepository(RepositoryContext repositoryContext) : RepositoryBase<Agent>(repositoryContext), IAgentRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Agent>> GetAllAgentsAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.Name)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Agent> GetAgentByIdAsync(Guid AgentId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Agent => Agent.Id.Equals(AgentId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Agent> GetAgentWithDetailsAsync(Guid AgentId)
        {
            return await FindByCondition(
                Agent => Agent.Id.Equals(AgentId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateAgent(Agent Agent)
        {
            Create(Agent);
        }

        public void UpdateAgent(Agent Agent)
        {
            Update(Agent);
        }

        public void DeleteAgent(Agent Agent)
        {
            Delete(Agent);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

