using Domain.Entities;
using Domain.SeedWork;

namespace Contracts.RepositoryInterfaces
{
    public interface IContactMessageRepository : IRepositoryContract<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync(CancellationToken cancellationToken = default);
        Task<ContactMessage> GetContactMessageByIdAsync(Guid ContactMessageId, CancellationToken cancellationToken = default);
        Task<ContactMessage> GetContactMessageWithDetailsAsync(Guid ContactMessageId);
        void CreateContactMessage(ContactMessage ContactMessage);
        void UpdateContactMessage(ContactMessage ContactMessage);
        void DeleteContactMessage(ContactMessage ContactMessage);
        void Save();
    }
}
