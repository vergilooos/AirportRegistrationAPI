using AirportRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportRegistration.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    // from the dependency injection container
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // DbSet people table
    public DbSet<Person> People => Set<Person>();

    // airports table
    public DbSet<Airport> Airports => Set<Airport>();

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //primary key of Airport to be the airport code
        modelBuilder.Entity<Airport>()
            .HasKey(a => a.Code);

        // one Airport has many People
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Airport)
            .WithMany(a => a.People)
            .HasForeignKey(p => p.AirportCode);
    }
}
