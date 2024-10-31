namespace MarktguruApi
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Models;
    using Models.Product;

    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
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