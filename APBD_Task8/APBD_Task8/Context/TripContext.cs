using APBD_Task8.Models;
using Microsoft.EntityFrameworkCore;
using APBD_Task8.Models;

namespace APBD_Task8.Context
{
    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientTrip> ClientTrips { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryTrip> CountryTrips { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasKey(c => c.IdClient);

            modelBuilder.Entity<ClientTrip>()
                .HasKey(ct => new { ct.IdClient, ct.IdTrip });

            modelBuilder.Entity<Country>()
                .HasKey(c => c.IdCountry);

            modelBuilder.Entity<CountryTrip>()
                .HasKey(ct => new { ct.IdCountry, ct.IdTrip });

            modelBuilder.Entity<Trip>()
                .HasKey(t => t.IdTrip);
        }
    }
}