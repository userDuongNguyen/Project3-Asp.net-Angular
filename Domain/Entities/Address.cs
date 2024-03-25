using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Address")]
    public class Address : IAggregateRoot
    {

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Street must not be null")]
        public required string Street { get; set; }
        [Required(ErrorMessage = "City must not be null")]
        public required string City { get; set; }
        [Required(ErrorMessage = "House Number must not be null")]
        public int StreetNumber { get; set; }
        [Required(ErrorMessage = "Postal code must be included")]
        public required string PostalCode { get; set; }
        public override string ToString()
        {
            return Street + " " + StreetNumber + ", " + PostalCode + " " + City;
        }
    }
}
