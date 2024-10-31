namespace MarktguruApi
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Models;
    using Models.Product;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IBaseObject baseObject)
                {
                    continue;
                }
                DateTime now = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Added:
                        baseObject.CreatedAt = now;
                        baseObject.UpdatedAt = now;
                        break;
                    case EntityState.Modified:
                        baseObject.UpdatedAt = now;
                        break;
                }
                baseObject.Version++;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}