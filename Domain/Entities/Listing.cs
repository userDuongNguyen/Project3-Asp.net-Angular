using Domain.SeedWork;

namespace Domain.Entities
{
    public class Listing : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string? Headline { get; set; }
        public string? PropertyDescription { get; set; }
        public string? PositionDescription { get; set; }
        public float LandSize { get; set; }
        public float RoomNumber { get; set; }
        public virtual EnergyEfficiencyClass? EfficiencyClass { get; set; }
        public virtual AccommodationType AccommodationType { get; set; }
        public virtual ResidenceCertificate ResidenceCertificate { get; set; }
        public virtual ContactDetail? ContactDetail { get; set; }
        public virtual Property? Property { get; set; }
        public virtual RentalFees? RentalFees { get; set; }
        public virtual User? User { get; set; }

    }
}
