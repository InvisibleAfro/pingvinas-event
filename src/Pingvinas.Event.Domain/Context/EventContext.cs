using Microsoft.EntityFrameworkCore;
using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Domain.Context;

public class EventContext : DbContext
{
    public DbSet<PingvinEvent> PingvinEvents { get; set; } = null!;
    public DbSet<Participant> Participants { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public EventContext(DbContextOptions<EventContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const int defaultIdentifierLength = 128;
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd().HasMaxLength(defaultIdentifierLength);
        });

        modelBuilder.Entity<PingvinEvent>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd().HasMaxLength(defaultIdentifierLength);
            entity.HasMany(x => x.Participants).WithOne(x => x.PingvinEvent).HasForeignKey(nameof(Participant));
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd().HasMaxLength(defaultIdentifierLength);
        });
    }
}