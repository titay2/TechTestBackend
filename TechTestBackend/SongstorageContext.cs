using Microsoft.EntityFrameworkCore;

namespace TechTestBackend;

public class SongstorageContext : DbContext
{
    public SongstorageContext(DbContextOptions<SongstorageContext> options)
        : base(options)
    {
    }

    public DbSet<Soptifysong> Songs { get; set; }
}