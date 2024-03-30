using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.PostDto
{
    public class RentalFeePostDto
    {
        public int Id { get; set; }
        [Required]
        public float BasicRent { get; set; }
        public float ExtraCost { get; set; }
        [Required]
        public float Deposit { get; set; }
        public float HeatingCost { get; set; }

    }
}
