using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ContactMessage")]
    public class ContactMessage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        [ForeignKey(nameof(Agent))]
        public int AgentId { get; set; }


    }
}
