using Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ContactDetail")]
    public class ContactDetail
    {
        public int Id { get; set; }
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
