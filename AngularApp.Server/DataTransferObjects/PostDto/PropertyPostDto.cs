using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DataTransferObjects.PostDto
{
    public class PropertyPostDto
    {
        public int Id { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
        public DateTime? VacantTime { get; set; }
        [ForeignKey("FK_Property_Address_AddressId")]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Street must not be null")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "City must not be null")]
        public string? City { get; set; }
        [Required(ErrorMessage = "House Number must not be null")]
        public int StreetNumber { get; set; }
        [Required(ErrorMessage = "Postal code must be included")]
        public string? PostalCode { get; set; }
        [ForeignKey("FK_Property_AccommodationDetail_AccommodationDetailId")]
        public int AccommodationDetailId { get; set; }
        public virtual List<LivingDetail>? LivingDetails { get; set; }
        public virtual Facility? Facility { get; set; } = SeedWork.Facility.Simple;
        public virtual PetAllowance? PetAllowance { get; set; } = SeedWork.PetAllowance.NotAllow;
        public virtual HeatingType? HeatingType { get; set; } = SeedWork.HeatingType.None;
        [Range(1, int.MaxValue, ErrorMessage = "Number of Bathroom must be at least 1")]
        public int BathroomNumber { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Number of Bedroom must be at least 1")]
        public int BedroomNumber { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Floor { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int MaxFloor { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive number allowed")]
        public float UsableArea { get; set; }
        public int ParkingNumber { get; set; }
        public float ParkingRent { get; set; }
        public virtual ParkingType? ParkingType { get; set; }
        public virtual InternetSpeed? InternetSpeed { get; set; }
    }
}
