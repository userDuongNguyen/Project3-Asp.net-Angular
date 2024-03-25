using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class SubcriptionRepository(RepositoryContext repositoryContext) : RepositoryBase<Subcription>(repositoryContext), ISubcriptionRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Subcription>> GetAllSubcriptionsAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.SubcriptionType)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Subcription> GetSubcriptionByIdAsync(Guid SubcriptionId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Subcription => Subcription.Id.Equals(SubcriptionId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Subcription> GetSubcriptionWithDetailsAsync(Guid SubcriptionId)
        {
            return await FindByCondition(
                Subcription => Subcription.Id.Equals(SubcriptionId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateSubcription(Subcription Subcription)
        {
            Create(Subcription);
        }

        public void UpdateSubcription(Subcription Subcription)
        {
            Update(Subcription);
        }

        public void DeleteSubcription(Subcription Subcription)
        {
            Delete(Subcription);
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

