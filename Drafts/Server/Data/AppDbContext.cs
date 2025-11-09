using Microsoft.EntityFrameworkCore;
using FootballCoachApp.Server.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<PositionPreference> PositionPreferences { get; set; }
    public DbSet<InjuryRecord> InjuryRecords { get; set; }
    public DbSet<PlayerAvailability> PlayerAvailabilityRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Optional: configure relationships, constraints, enums, etc.

        modelBuilder.Entity<Player>()
            .HasMany(p => p.PositionPreferences)
            .WithOne(pp => pp.Player)
            .HasForeignKey(pp => pp.PlayerId);

        modelBuilder.Entity<Player>()
            .HasMany(p => p.InjuryHistory)
            .WithOne(ir => ir.Player)
            .HasForeignKey(ir => ir.PlayerId);

        modelBuilder.Entity<Player>()
            .HasMany(p => p.AvailabilityRecords)
            .WithOne(ar => ar.Player)
            .HasForeignKey(ar => ar.PlayerId);

        modelBuilder.Entity<Event>()
            .HasMany(e => e.PlayerAvailability)
            .WithOne(pa => pa.Event)
            .HasForeignKey(pa => pa.EventId);
    }
}