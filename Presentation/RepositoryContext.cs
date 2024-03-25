using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
    {
        public DbSet<Address> DbSetAddress { get; set; } = default!;
        public DbSet<AccommodationDetail> DbSetAccommodationDetail { get; set; } = default!;
        public DbSet<Agent> DbSetAgent { get; set; } = default!;
        public DbSet<ContactDetail> DbSetContactDetail { get; set; } = default!;
        public DbSet<ContactMessage> DbSetContactMessage { get; set; } = default!;
        public DbSet<Listing> DbSetListing { get; set; } = default!;
        public DbSet<Property> DbSetProperty { get; set; } = default!;
        public DbSet<RentalFees> DbSetRentalFees { get; set; } = default!;
        public DbSet<Subcription> DbSetSubcription { get; set; } = default!;
        public DbSet<User> DbSetUser { get; set; } = default!;
        public DbSet<Wallet> DbSetWallet { get; set; } = default!;


    }
}

