using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LPMoneyTracker.Data
{
    public class LPMoneyTrackerContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public LPMoneyTrackerContext(DbContextOptions<LPMoneyTrackerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().ToContainer("Transactions").HasPartitionKey(t => t.TransId);
            modelBuilder.Entity<Category>().ToContainer("Categories").HasPartitionKey(c => c.id);

            base.OnModelCreating(modelBuilder);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }
    }
}
