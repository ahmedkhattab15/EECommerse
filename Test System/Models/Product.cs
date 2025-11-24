using System.ComponentModel.DataAnnotations.Schema;

namespace Test_System.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public string MainImg { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
