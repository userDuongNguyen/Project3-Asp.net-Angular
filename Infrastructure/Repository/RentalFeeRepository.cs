using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RentalFeeRepository
        (DataContext repositoryContext) : RepositoryBase<RentalFee>(repositoryContext),
    IRentalFeeRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<RentalFee>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<RentalFee> GetByIdAsync(int RentalFeeId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        RentalFee => RentalFee.Id.Equals(RentalFeeId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<RentalFee> GetWithDetailsAsync(int RentalFeeId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                RentalFee => RentalFee.Id.Equals(RentalFeeId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(RentalFee RentalFee)
        {
            Create(RentalFee);
        }

        public void UpdateDetail(RentalFee RentalFee)
        {
            Update(RentalFee);
        }

        public void DeleteDetail(RentalFee RentalFee)
        {
            Delete(RentalFee);
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

