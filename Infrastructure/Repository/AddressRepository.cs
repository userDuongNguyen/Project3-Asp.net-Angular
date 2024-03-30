using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AddressRepository
        (DataContext repositoryContext) : RepositoryBase<Address>(repositoryContext),
    IAddressRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Address>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Address> GetByIdAsync(int AddressId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Address => Address.Id.Equals(AddressId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Address> GetWithDetailsAsync(int AddressId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                Address => Address.Id.Equals(AddressId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(Address Address)
        {
            Create(Address);
        }

        public void UpdateDetail(Address Address)
        {
            Update(Address);
        }

        public void DeleteDetail(Address Address)
        {
            Delete(Address);
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

