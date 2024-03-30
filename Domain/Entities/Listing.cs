using Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Listing")]
    public class Listing
    {
        public int Id { get; set; }
        public string? Headline { get; set; }
        public string? PropertyDescription { get; set; }
        public float LandSize { get; set; }
        public float RoomNumber { get; set; }
        public virtual EnergyEfficiencyClass? EfficiencyClass { get; set; }
        public virtual AccommodationType AccommodationType { get; set; }
        public virtual ResidenceCertificate ResidenceCertificate { get; set; }
        [ForeignKey(nameof(ContactDetail))]

        public virtual ContactDetail? ListingContactDetail { get; set; }
        [ForeignKey(nameof(Property))]
        public virtual Property? ListingProperty { get; set; }
        [ForeignKey(nameof(RentalFee))]
        public virtual RentalFee? ListingRentalFee { get; set; }
        [ForeignKey(nameof(User))]
        public virtual User? ListingUser { get; set; }

    }
}
