using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Test_System.Models
{
    [PrimaryKey(nameof(ProductID), nameof(Color))]
    public class ProductColor 
    {
        public int ProductID { get; set; }

        public Product product { get; set; } = null!;

        public string Color { get; set; } = string.Empty;
    }
}
