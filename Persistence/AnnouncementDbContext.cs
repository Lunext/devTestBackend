using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class AnnouncementDbContext : DbContext
{
    public virtual DbSet<Announcement> Announcements { get; set; } = null!;

    public AnnouncementDbContext()
    {

    }

    public AnnouncementDbContext(DbContextOptions<AnnouncementDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host = localhost; Port=5432;Database=announcementdb;Username=postgres;Password=Euren002");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}