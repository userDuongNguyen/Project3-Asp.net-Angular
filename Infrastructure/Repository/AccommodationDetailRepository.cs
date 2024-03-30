using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AccommodationDetailRepository
        (DataContext repositoryContext) : RepositoryBase<AccommodationDetail>(repositoryContext),
    IAccommodationDetailRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<AccommodationDetail>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<AccommodationDetail> GetByIdAsync(int AccommodationDetailId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        AccommodationDetail => AccommodationDetail.Id.Equals(AccommodationDetailId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<AccommodationDetail> GetWithDetailsAsync(int AccommodationDetailId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                AccommodationDetail => AccommodationDetail.Id.Equals(AccommodationDetailId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(AccommodationDetail AccommodationDetail)
        {
            Create(AccommodationDetail);
        }

        public void UpdateDetail(AccommodationDetail AccommodationDetail)
        {
            Update(AccommodationDetail);
        }

        public void DeleteDetail(AccommodationDetail AccommodationDetail)
        {
            Delete(AccommodationDetail);
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

