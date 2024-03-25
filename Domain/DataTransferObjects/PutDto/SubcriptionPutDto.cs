using Domain.SeedWork;

namespace Domain.DataTransferObjects.PutDto
{
    public class SubcriptionPutDto
    {
        public Guid Id { get; }
        public virtual SubcriptionPack? SubcriptionPack { get; }
        public virtual DateTime? SubcriptionDate { get; set; }
        public int SubcriptionDuration { get; set; }
        public virtual AccountTypes? SubcriptionType { get; set; }
    }
}
