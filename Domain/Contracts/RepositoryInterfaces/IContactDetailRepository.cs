using Domain.Entities;
using Domain.SeedWork;

namespace Domain.Contracts.RepositoryInterfaces
{
    public interface IContactDetailRepository : IRepositoryContract<ContactDetail>
    {
        Task<IEnumerable<ContactDetail>> GetAllAsync(CancellationToken cancellationToken);
        Task<ContactDetail> GetByIdAsync(int Id, CancellationToken cancellationToken);
        Task<ContactDetail> GetWithDetailsAsync(int Id, CancellationToken cancellationToken);
        void CreateDetail(ContactDetail ContactDetail);
        void UpdateDetail(ContactDetail ContactDetail);
        void DeleteDetail(ContactDetail ContactDetail);
    }
}
