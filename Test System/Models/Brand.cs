namespace Test_System.Models
{
    public class Brand
    { 
        public int id { get; set; }
        public string name { get; set; } = string.Empty;  // not null
        public string? description { get; set; } // null 
        public Boolean Status { get; set; }
        public string Img { get; set; }

        public List<Product> products { get; set; }
    }
}
