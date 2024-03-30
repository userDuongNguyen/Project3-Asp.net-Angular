

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("RentalFee")]
    public class RentalFee
    {
        public int Id { get; set; }
        public float BasicRent { get; set; }
        public float ExtraCost { get; set; }
        public float Deposit { get; set; }
        public float HeatingCost { get; set; }

    }
}
