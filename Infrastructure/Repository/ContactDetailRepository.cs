using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ContactDetailRepository
        (DataContext repositoryContext) : RepositoryBase<ContactDetail>(repositoryContext),
    IContactDetailRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<ContactDetail>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactDetail> GetByIdAsync(int ContactDetailId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        ContactDetail => ContactDetail.Id.Equals(ContactDetailId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactDetail> GetWithDetailsAsync(int ContactDetailId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                ContactDetail => ContactDetail.Id.Equals(ContactDetailId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(ContactDetail ContactDetail)
        {
            Create(ContactDetail);
        }

        public void UpdateDetail(ContactDetail ContactDetail)
        {
            Update(ContactDetail);
        }

        public void DeleteDetail(ContactDetail ContactDetail)
        {
            Delete(ContactDetail);
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

