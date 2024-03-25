using Domain.SeedWork;

namespace Domain.Entities
{
    public class ContactDetail : IAggregateRoot
    {
        public Guid Id { get; set; }
        public virtual Salutation? Salutation { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public override string ToString()
        {
            return Salutation + " " + FirstName + " " + LastName;
        }
    }
}
