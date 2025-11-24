using Microsoft.AspNetCore.Identity.UI.Services;
using Test_System.Models;

namespace Test_System.ViewModel
{
    public class CategoriesWithBrandVM
    {
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Brand> brands { get; set; }
    }
}
