using Domain.Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ContactMessageRepository
        (DataContext repositoryContext) : RepositoryBase<ContactMessage>(repositoryContext),
    IContactMessageRepository, IDisposable
    {
#nullable disable

        private readonly DataContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<ContactMessage>> GetAllAsync
            (CancellationToken cancellationToken = default)
        {

            return await FindAll().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactMessage> GetByIdAsync(int ContactMessageId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        ContactMessage => ContactMessage.Id.Equals(ContactMessageId))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactMessage> GetWithDetailsAsync(int ContactMessageId, CancellationToken cancellationToken = default)
        {
            return await FindByCondition(
                ContactMessage => ContactMessage.Id.Equals(ContactMessageId))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public void CreateDetail(ContactMessage ContactMessage)
        {
            Create(ContactMessage);
        }

        public void UpdateDetail(ContactMessage ContactMessage)
        {
            Update(ContactMessage);
        }

        public void DeleteDetail(ContactMessage ContactMessage)
        {
            Delete(ContactMessage);
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

