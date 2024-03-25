using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace Repository.Repository
{
    public class AddressRepository(RepositoryContext repositoryContext) : RepositoryBase<Address>(repositoryContext), IAddressRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<Address>> GetAllAddressAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.City)
               .OrderBy(ad => ad.Street)
               .OrderBy(ad => ad.StreetNumber)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Address> GetAddressByIdAsync(Guid AddressId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        Address => Address.Id.Equals(AddressId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Address> GetAddressWithDetailsAsync(Guid AddressId)
        {
            return await FindByCondition(
                Address => Address.Id.Equals(AddressId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateAddress(Address Address)
        {
            Create(Address);
        }

        public void UpdateAddress(Address Address)
        {
            Update(Address);
        }

        public void DeleteAddress(Address Address)
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


