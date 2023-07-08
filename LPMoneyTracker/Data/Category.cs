using System.ComponentModel.DataAnnotations;

namespace LPMoneyTracker.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
