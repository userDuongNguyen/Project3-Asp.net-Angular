using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class SubcriptionRepository
        (DataContext repositoryContext) : RepositoryBase<Subcription>(repositoryContext),
    ISubcriptionRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Subcription>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Subcription> GetByIdAsync(int SubcriptionId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Subcription => Subcription.Id.Equals(SubcriptionId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Subcription> GetWithDetailsAsync(int SubcriptionId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                Subcription => Subcription.Id.Equals(SubcriptionId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(Subcription Subcription)
        {
            Create(Subcription);
        }

        public void UpdateDetail(Subcription Subcription)
        {
            Update(Subcription);
        }

        public void DeleteDetail(Subcription Subcription)
        {
            Delete(Subcription);
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

