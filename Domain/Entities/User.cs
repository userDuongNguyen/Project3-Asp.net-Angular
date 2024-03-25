using Domain.SeedWork;


namespace Domain.Entities
{
    public class User : IAggregateRoot
    {
        public Guid Id { get; set; }
        public ContactDetail? ContactDetail { get; set; }
        public Guid ContactDetailId { get; set; }
        public string? Password { get; set; }
        public virtual AgentRole? AgentRole { get; set; }
        public Wallet? Wallet { get; set; }
        public virtual List<Subcription>? Subcriptions { get; set; }

    }
}
