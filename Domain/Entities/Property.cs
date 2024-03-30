using Domain.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Property")]
    public class Property
    {
        public int Id { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
        public DateTime? VacantTime { get; set; }
        [ForeignKey(nameof(Address))]
        public int? AddressId { get; set; }
        public virtual Address? AddressDetail { get; set; }
        [ForeignKey(nameof(AccommodationDetail))]
        public int? AccommodationDetailId { get; set; }
        public virtual AccommodationDetail? AccommodationDetails { get; set; }

    }
}
