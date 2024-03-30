using Domain.Entities;
using Domain.SeedWork;

namespace Domain.DataTransferObjects.PostDto
{
    public class ListingPostDto
    {
        public int Id { get; set; }
        public string? Headline { get; set; }
        public string? PropertyDescription { get; set; }
        public string? PositionDescription { get; set; }
        public float LandSize { get; set; }
        public float RoomNumber { get; set; }
        public virtual EnergyEfficiencyClass? EfficiencyClass { get; set; }
        public virtual AccommodationType AccommodationType { get; set; }
        public virtual ResidenceCertificate ResidenceCertificate { get; set; }
        public int ContactDetailId { get; set; }
        public virtual Salutation? Salutation { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public virtual Property? Property { get; set; }
        public virtual RentalFee? RentalFee { get; set; }
        public virtual User? User { get; set; }
    }
}
