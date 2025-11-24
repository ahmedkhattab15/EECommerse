using System.ComponentModel.DataAnnotations;

namespace Test_System.Models
{
    public class Category
    {
        public int id { get; set; }
        [Requierd]
        [MinLength(3)]
        [MaxLength(100)]
        public string name { get; set; } = string.Empty;  // not null

        [MaxLength(1000)]
        public string? description { get; set; } // null 
        public Boolean Status { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
