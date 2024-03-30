using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<AccommodationDetail> AccommodationDetail { get; set; } = default!;

        public DbSet<Address> Address { get; set; } = default!;

        public DbSet<Agent> Agent { get; set; } = default!;

        public DbSet<ContactDetail> ContactDetail { get; set; } = default!;

        public DbSet<ContactMessage> ContactMessage { get; set; } = default!;

        public DbSet<Listing> Listing { get; set; } = default!;

        public DbSet<Wallet> Wallet { get; set; } = default!;

        public DbSet<User> User { get; set; } = default!;

        public DbSet<RentalFee> RentalFee { get; set; } = default!;

        public DbSet<Property> Property { get; set; } = default!;
    }
}