using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DataTransferObjects.PostDto
{
    public class ContactMessagePostDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        [ForeignKey("FK_ContactMessage_Agent_AgentId")]
        public required Guid AgentId { get; set; }
    }
}
