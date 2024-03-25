using Domain.SeedWork;

namespace Domain.DataTransferObjects.PostDto
{
    public class ContactDetailPostDto
    {
        public Guid Id { get; set; }
        public virtual Salutation? Salutation { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
