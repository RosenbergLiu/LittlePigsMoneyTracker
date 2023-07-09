using System.ComponentModel.DataAnnotations;

namespace LPMoneyTracker.Data
{
    public class Transaction
    {
        [Key]
        public string? TransId { get; set; }
        [Required]
        public DateTime TransDate { get; set; }
        [Required]
        public string? Merchant { get; set; }
        [Required]
        public double Amount { get; set; }
        public string? CategoryName { get; set; }
        public string? Note { get; set; }
        public string? Account { get; set; }

        [Required]
        public bool IsSubscription { get; set; } = false;

        [Required]
        public bool IsAccident { get; set; } = false;
    }
}
