using Domain.SeedWork;

namespace Domain.DataTransferObjects.GetDto
{
    public class PropertyGetDto
    {
        public Guid Id { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
        public DateTime? VacantTime { get; set; }
    }
}
