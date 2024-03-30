using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Wallet")]
    public class Wallet
    {
        public int Id { get; set; }
        public float Balance { get; set; }

    }
}
