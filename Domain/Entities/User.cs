using Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        [ForeignKey(nameof(ContactDetail))]
        public int ContactDetailId { get; set; }
        public virtual ContactDetail? ContactDetails { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public virtual AgentRole? AgentRole { get; set; }
        [ForeignKey(nameof(Wallet))]
        public int WalletId { get; set; }
        public virtual Wallet? WalletDetail { get; set; }
        [ForeignKey(nameof(Subcription))]
        public virtual ICollection<Subcription>? Subcriptions { get; set; }

    }
}
