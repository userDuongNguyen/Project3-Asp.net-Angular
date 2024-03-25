using Contracts.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProjectInfrastructure.IRepository;

namespace ProjectApplication.Repository
{
    public class ContactDetailRepository(RepositoryContext repositoryContext) : RepositoryBase<ContactDetail>(repositoryContext), IContactDetailRepository, IDisposable
    {
#nullable disable
        private readonly RepositoryContext context = repositoryContext;
        private bool disposed = false;

        public async Task<IEnumerable<ContactDetail>> GetAllContactDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await FindAll()
               .OrderBy(ad => ad.FirstName)
               .OrderBy(ad => ad.LastName)
               .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactDetail> GetContactDetailByIdAsync(Guid ContactDetailId, CancellationToken cancellationToken = default)
        {

            return await FindByCondition(
                        ContactDetail => ContactDetail.Id.Equals(ContactDetailId)
                        )
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<ContactDetail> GetContactDetailWithDetailsAsync(Guid ContactDetailId)
        {
            return await FindByCondition(
                ContactDetail => ContactDetail.Id.Equals(ContactDetailId)
                )

                .FirstOrDefaultAsync();
        }

        public void CreateContactDetail(ContactDetail ContactDetail)
        {
            Create(ContactDetail);
        }

        public void UpdateContactDetail(ContactDetail ContactDetail)
        {
            Update(ContactDetail);
        }

        public void DeleteContactDetail(ContactDetail ContactDetail)
        {
            Delete(ContactDetail);
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

