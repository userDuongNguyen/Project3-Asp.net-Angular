using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class PropertyRepository(RepositoryContext repositoryContext) : RepositoryBase<Property>(repositoryContext), IPropertyRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Property>> GetAllPropertysAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.VacantTime)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Property> GetPropertyByIdAsync(Guid PropertyId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Property => Property.Id.Equals(PropertyId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Property> GetPropertyWithDetailsAsync(Guid PropertyId)
        {
            return await FindByCondition(
                Property => Property.Id.Equals(PropertyId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateProperty(Property Property)
        {
            Create(Property);
        }

        public void UpdateProperty(Property Property)
        {
            Update(Property);
        }

        public void DeleteProperty(Property Property)
        {
            Delete(Property);
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

