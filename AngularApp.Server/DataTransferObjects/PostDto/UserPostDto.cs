using Domain.Entities;
using Domain.SeedWork;

namespace Domain.DataTransferObjects.PostDto
{
    public class UserPostDto
    {
        public int Id { get; set; }
        public ContactDetail? ContactDetail { get; set; }
        public int   ContactDetailId { get; set; }
        public string? Password { get; set; }
        public virtual AgentRole? AgentRole { get; set; }
        public int WalletId { get; set; }
        public float Balance = 0;
    }
}
