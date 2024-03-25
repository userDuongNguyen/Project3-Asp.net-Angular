using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DataTransferObjects.PostDto
{
    public class AgentPostDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        [MinLength(3, ErrorMessage = "Name can be shorter than 3 characters")]
        public string? Name { get; set; }
        public virtual List<User>? Users { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, ErrorMessage = "Description can't be longer than 300 characters")]
        [MinLength(10, ErrorMessage = "Description can be shorter than 10 characters")]
        public string? Description { get; set; }
        public string? Phone { get; set; }
        [ForeignKey("FK_Agent_Wallet_WalletId")]
        public Guid WalletId = Guid.NewGuid();
        public float Balance = 0f;
        [ForeignKey("FK_Agent_Address_AddressId")]
        public required Guid AddressId { get; set; }
        [Required(ErrorMessage = "Street must not be null")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "City must not be null")]
        public string? City { get; set; }
        [Required(ErrorMessage = "House Number must not be null")]
        public int StreetNumber { get; set; }
        [Required(ErrorMessage = "Postal code must be included")]
        public string? PostalCode { get; set; }
    }
}
