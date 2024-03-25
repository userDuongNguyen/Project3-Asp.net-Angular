using Domain.SeedWork;

namespace Domain.DataTransferObjects.PutDto
{
    public class ListingPutDto
    {
        public Guid Id { get; }
        public string? Headline { get; set; }
        public string? PropertyDescription { get; set; }
        public string? PositionDescription { get; set; }
        public float LandSize { get; set; }
        public float RoomNumber { get; set; }
        public virtual EnergyEfficiencyClass? EfficiencyClass { get; set; }
        public virtual AccommodationType AccommodationType { get; set; }
        public virtual ResidenceCertificate ResidenceCertificate { get; set; }
    }
}
