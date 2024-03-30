namespace Domain.DataTransferObjects.GetDto
{
    public class ContactMessageGetDto
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}
