using Domain.SeedWork;

namespace Domain.Entities
{
    public class Property : IAggregateRoot
    {
        public Guid Id { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
        public DateTime? VacantTime { get; set; }
        public virtual Address? Address { get; set; }
        public virtual AccommodationDetail? AccommodationDetail { get; set; }

    }
}
