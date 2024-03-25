using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class RentalFeesRepository(RepositoryContext repositoryContext) : RepositoryBase<RentalFees>(repositoryContext), IRentalFeesRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<RentalFees>> GetAllRentalFeessAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.BasicRent)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<RentalFees> GetRentalFeesByIdAsync(Guid RentalFeesId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        RentalFees => RentalFees.Id.Equals(RentalFeesId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<RentalFees> GetRentalFeesWithDetailsAsync(Guid RentalFeesId)
        {
            return await FindByCondition(
                RentalFees => RentalFees.Id.Equals(RentalFeesId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateRentalFees(RentalFees RentalFees)
        {
            Create(RentalFees);
        }

        public void UpdateRentalFees(RentalFees RentalFees)
        {
            Update(RentalFees);
        }

        public void DeleteRentalFees(RentalFees RentalFees)
        {
            Delete(RentalFees);
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

