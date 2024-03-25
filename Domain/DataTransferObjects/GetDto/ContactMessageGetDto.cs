namespace Domain.DataTransferObjects.GetDto
{
    public class ContactMessageGetDto
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}
