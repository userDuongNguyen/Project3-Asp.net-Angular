using Domain.Entities;
using Domain.SeedWork;

namespace Domain.DataTransferObjects.PostDto
{
    public class UserPostDto
    {
        public Guid Id { get; set; }
        public ContactDetail? ContactDetail { get; set; }
        public Guid ContactDetailId { get; set; }
        public string? Password { get; set; }
        public virtual AgentRole? AgentRole { get; set; }
        public Guid WalletId { get; set; }
        public float Balance = 0;
    }
}
