using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PropertyRepository
        (DataContext repositoryContext) : RepositoryBase<Property>(repositoryContext),
    IPropertyRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Property>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Property> GetByIdAsync(int PropertyId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Property => Property.Id.Equals(PropertyId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Property> GetWithDetailsAsync(int PropertyId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                Property => Property.Id.Equals(PropertyId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(Property Property)
        {
            Create(Property);
        }

        public void UpdateDetail(Property Property)
        {
            Update(Property);
        }

        public void DeleteDetail(Property Property)
        {
            Delete(Property);
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

