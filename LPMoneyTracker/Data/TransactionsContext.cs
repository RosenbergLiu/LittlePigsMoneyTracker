using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LPMoneyTracker.Data
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext (DbContextOptions<TransactionsContext> options)
            : base(options)
        {
        }

        public DbSet<LPMoneyTracker.Data.Transaction> Transaction { get; set; } = default!;
        
    }
}
