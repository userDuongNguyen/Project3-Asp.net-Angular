using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.PutDto
{
    public class ContactDetailPutDto
    {
        public int Id { get; }
        public virtual Salutation? Salutation { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
