namespace Domain.DataTransferObjects.PutDto
{
    public class AgentPutDto
    {
        public int Id { get; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
    }
}
