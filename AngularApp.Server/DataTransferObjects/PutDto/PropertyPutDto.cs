using Domain.SeedWork;

namespace Domain.DataTransferObjects.PutDto
{
    public class PropertyPutDto
    {
        public int Id { get; }
        public virtual PropertyType? PropertyType { get; set; }
        public DateTime? VacantTime { get; set; }
    }
}
