using Domain.SeedWork;

namespace Domain.DataTransferObjects.GetDto
{
    public class SubcriptionGetDto
    {
        public Guid Id { get; set; }
        public virtual SubcriptionPack? SubcriptionPack { get; set; }
        public virtual DateTime? SubcriptionDate { get; set; }
        public int SubcriptionDuration { get; set; }
        public virtual AccountTypes? SubcriptionType { get; set; }
    }
}
