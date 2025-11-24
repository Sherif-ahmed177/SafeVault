using Microsoft.EntityFrameworkCore;
using SafeVault.Models;

namespace SafeVault.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<UserRecord> Users => Set<UserRecord>();
    public DbSet<Note> Notes => Set<Note>();
}

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        if (!db.Users.Any())
        {
            db.Users.AddRange(
                new UserRecord { Username = "alice", Email = "alice@example.com" },
                new UserRecord { Username = "bob", Email = "bob@example.com" }
            );
            db.SaveChanges();
        }
    }
}
