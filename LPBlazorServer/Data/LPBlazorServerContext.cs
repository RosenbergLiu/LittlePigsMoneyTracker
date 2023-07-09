using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LPBlazorServer.Data;

namespace LPBlazorServer.Data
{
    public class LPBlazorServerContext : DbContext
    {
        public LPBlazorServerContext (DbContextOptions<LPBlazorServerContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction>? Transactions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().ToContainer("Transactions").HasPartitionKey(t => t.TransId);
            //modelBuilder.Entity<Category>().ToContainer("Categories").HasPartitionKey(c => c.CategoryId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
