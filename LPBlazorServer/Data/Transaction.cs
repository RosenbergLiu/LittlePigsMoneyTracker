using System.ComponentModel.DataAnnotations;

namespace LPBlazorServer.Data
{
    public class Transaction
    {
        [Key]
        public string? TransId { get; set; } = Guid.NewGuid().ToString();
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
        [Required]
        public bool IsTransfer { get; set; } = false;
    }
}
