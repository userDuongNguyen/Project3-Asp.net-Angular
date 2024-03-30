using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AgentRepository
        (DataContext repositoryContext) : RepositoryBase<Agent>(repositoryContext),
    IAgentRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Agent>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Agent> GetByIdAsync(int AgentId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Agent => Agent.Id.Equals(AgentId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Agent> GetWithDetailsAsync(int AgentId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                Agent => Agent.Id.Equals(AgentId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(Agent Agent)
        {
            Create(Agent);
        }

        public void UpdateDetail(Agent Agent)
        {
            Update(Agent);
        }

        public void DeleteDetail(Agent Agent)
        {
            Delete(Agent);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
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

