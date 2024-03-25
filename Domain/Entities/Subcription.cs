using Domain.SeedWork;

namespace Domain.Entities
{
    public class Subcription : IAggregateRoot
    {
        public Guid Id { get; set; }
        public virtual SubcriptionPack? SubcriptionPack { get; set; }
        public virtual DateTime? SubcriptionDate { get; set; }
        public int SubcriptionDuration { get; set; }
        public virtual AccountTypes? SubcriptionType { get; set; }
    }
}
