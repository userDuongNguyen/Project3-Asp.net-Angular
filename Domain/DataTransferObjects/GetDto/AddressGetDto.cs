namespace Domain.DataTransferObjects.GetDto
{
    public class AddressGetDto
    {
        public required Guid Id { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public int StreetNumber { get; set; }

        public string? PostalCode { get; set; }
    }
}
