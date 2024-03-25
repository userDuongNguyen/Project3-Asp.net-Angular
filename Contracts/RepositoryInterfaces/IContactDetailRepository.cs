using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IContactDetailRepository : IRepositoryContract<ContactDetail>
    {
        Task<IEnumerable<ContactDetail>> GetAllContactDetailsAsync(CancellationToken cancellationToken = default);
        Task<ContactDetail> GetContactDetailByIdAsync(Guid ContactDetailId, CancellationToken cancellationToken = default);
        Task<ContactDetail> GetContactDetailWithDetailsAsync(Guid ContactDetailId);
        void CreateContactDetail(ContactDetail ContactDetail);
        void UpdateContactDetail(ContactDetail ContactDetail);
        void DeleteContactDetail(ContactDetail ContactDetail);
        void Save();
    }
}
