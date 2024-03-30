using Domain.SeedWork;

namespace Domain.DataTransferObjects.GetDto
{
    public class ContactDetailGetDto
    {
        public int Id { get; set; }
        public virtual Salutation? Salutation { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
