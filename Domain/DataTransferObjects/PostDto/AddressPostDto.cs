using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.PostDto
{
    public class AddressPostDto
    {
#nullable disable
        public Guid AddressId { get; set; }
        [Required(ErrorMessage = "Street must not be null")]
        public required string Street { get; set; }
        [Required(ErrorMessage = "City must not be null")]
        public required string City { get; set; }
        [Required(ErrorMessage = "House Number must not be null")]
        public int StreetNumber { get; set; }
        [Required(ErrorMessage = "Postal code must be included")]
        public required string PostalCode { get; set; }
    }
}
