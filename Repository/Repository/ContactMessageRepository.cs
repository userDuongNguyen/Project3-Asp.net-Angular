using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class ContactMessageRepository(RepositoryContext repositoryContext) : RepositoryBase<ContactMessage>(repositoryContext), IContactMessageRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.Name)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactMessage> GetContactMessageByIdAsync(Guid ContactMessageId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        ContactMessage => ContactMessage.Id.Equals(ContactMessageId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactMessage> GetContactMessageWithDetailsAsync(Guid ContactMessageId)
        {
            return await FindByCondition(
                ContactMessage => ContactMessage.Id.Equals(ContactMessageId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateContactMessage(ContactMessage ContactMessage)
        {
            Create(ContactMessage);
        }

        public void UpdateContactMessage(ContactMessage ContactMessage)
        {
            Update(ContactMessage);
        }

        public void DeleteContactMessage(ContactMessage ContactMessage)
        {
            Delete(ContactMessage);
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

