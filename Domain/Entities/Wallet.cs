using Domain.SeedWork;

namespace Domain.Entities
{
    public class Wallet : IAggregateRoot
    {
        public Guid Id { get; set; }
        public float Balance { get; set; }

    }
}
