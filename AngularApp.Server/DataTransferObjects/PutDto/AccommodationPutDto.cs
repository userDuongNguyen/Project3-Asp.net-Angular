using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.PutDto
{
    public class AccommodationDetailPutDto
    {
        public int Id { get; }
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
