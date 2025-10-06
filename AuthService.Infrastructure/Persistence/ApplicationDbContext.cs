using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities

        // Exposes the Users table as a DbSet, retrieved dynamically via Set<T>().
        // This read-only property prevents accidental reassignment and follows best practice for EF Core + DDD.
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        // Optional: Automatically set audit fields (CreatedAt / UpdatedAt)
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is User &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    ((User)entry.Entity).GetType().GetProperty("CreatedAt")?.SetValue(entry.Entity, DateTime.UtcNow);
                else
                    ((User)entry.Entity).GetType().GetProperty("UpdatedAt")?.SetValue(entry.Entity, DateTime.UtcNow);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
