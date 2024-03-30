using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IContactMessageRepository : IRepositoryContract<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetAllAsync(CancellationToken cancellationToken);
        Task<ContactMessage> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<ContactMessage> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(ContactMessage ContactMessage);
        void UpdateDetail(ContactMessage ContactMessage);
        void DeleteDetail(ContactMessage ContactMessage);
    }
}