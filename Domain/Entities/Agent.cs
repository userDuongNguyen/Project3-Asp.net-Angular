using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Agent")]
    public class Agent
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        [MinLength(3, ErrorMessage = "Name can be shorter than 3 characters")]
        public string? Name { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, ErrorMessage = "Description can't be longer than 300 characters")]
        [MinLength(10, ErrorMessage = "Description can be shorter than 10 characters")]
        public string? Description { get; set; }
        public string? Phone { get; set; }
        [ForeignKey(nameof(Wallet.Id))]
        public int WalletId { get; set; }
        public virtual Wallet? Wallet { get; set; }
        [ForeignKey(nameof(Address.Id))]
        public int AddressId { get; set; }
        public virtual Address? Address { get; set; }
    }
}
