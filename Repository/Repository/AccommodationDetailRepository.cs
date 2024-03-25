using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace Repository.Repository
{
    public class AccommodationDetailRepository
        (RepositoryContext repositoryContext) : RepositoryBase<AccommodationDetail>(repositoryContext),
    IAccommodationDetailRepository, IDisposable
    {
#nullable disable

        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<AccommodationDetail>> GetAllAccommodationDetailsAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<AccommodationDetail> GetAccommodationDetailByIdAsync(Guid AccommodationDetailId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        AccommodationDetail => AccommodationDetail.Id.Equals(AccommodationDetailId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<AccommodationDetail> GetAccommodationDetailWithDetailsAsync(Guid AccommodationDetailId)
        {
            return await FindByCondition(
                AccommodationDetail => AccommodationDetail.Id.Equals(AccommodationDetailId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateAccommodationDetail(AccommodationDetail AccommodationDetail)
        {
            Create(AccommodationDetail);
        }

        public void UpdateAccommodationDetail(AccommodationDetail AccommodationDetail)
        {
            Update(AccommodationDetail);
        }

        public void DeleteAccommodationDetail(AccommodationDetail AccommodationDetail)
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

