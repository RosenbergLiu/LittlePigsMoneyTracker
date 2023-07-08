using Microsoft.EntityFrameworkCore;

namespace LPMoneyTracker.Data
{
    public class CategoriesContext : DbContext
    {
        public CategoriesContext(DbContextOptions<CategoriesContext> options)
            : base(options)
        {
        }

        public DbSet<LPMoneyTracker.Data.Category> Category { get; set; } = default!;
    }
}
