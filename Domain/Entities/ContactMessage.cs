using Domain.SeedWork;

namespace Domain.Entities
{
    public class ContactMessage : IAggregateRoot
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public Agent? Agent { get; set; }


    }
}
