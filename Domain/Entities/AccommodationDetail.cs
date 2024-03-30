using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("AccommodationDetail")]
    public class AccommodationDetail
    {
#nullable enable
        public required int Id { get; set; }
        public virtual ICollection<LivingDetail>? LivingDetails { get; set; }
        public virtual Facility? Facility { get; set; }
        public virtual PetAllowance? PetAllowance { get; set; }
        public virtual HeatingType? HeatingType { get; set; }
        public int BathroomNumber { get; set; }
        public int BedroomNumber { get; set; }
        public int Floor { get; set; }
        public int MaxFloor { get; set; }
        [Required(ErrorMessage = "Usable area is required")]
        public float UsableArea { get; set; }
        public int ParkingNumber { get; set; }
        public float ParkingRent { get; set; }
        public virtual ParkingType? ParkingType { get; set; }
        public virtual InternetSpeed? InternetSpeed { get; set; }

    }
}
