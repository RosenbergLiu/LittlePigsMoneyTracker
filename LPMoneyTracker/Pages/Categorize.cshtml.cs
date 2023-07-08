using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LPMoneyTracker.Data;

namespace LPMoneyTracker.Pages
{
    public class CategorizeModel : PageModel
    {
        private readonly LPMoneyTracker.Data.TransactionsContext _context;

        public CategorizeModel(LPMoneyTracker.Data.TransactionsContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Transaction != null)
            {
                Transaction = await _context.Transaction.ToListAsync();
            }
        }
    }
}
