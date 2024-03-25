namespace Domain.DataTransferObjects.GetDto
{
    public class AgentGetDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
    }
}
