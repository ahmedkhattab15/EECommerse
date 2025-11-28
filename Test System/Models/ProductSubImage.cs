using Microsoft.EntityFrameworkCore;

namespace Test_System.Models
{
    [PrimaryKey(nameof(ProductID), nameof(img))]

    public class ProductSubImage
    {
        internal string SubImg;

        public int ProductID { get; set; }
        public Product product { get; set; } = null!;
        public string img { get; set; } = string.Empty;


    }
}
