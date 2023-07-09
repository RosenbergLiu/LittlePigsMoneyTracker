using System.ComponentModel.DataAnnotations;

namespace LPMoneyTracker.Data
{
    public class Category
    {
        [Key]
        public string? id { get; set; }
        public string? CategoryName { get; set; }
    }
}
