using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Test_System.Data_Acssess;
using Test_System.Models;
using Test_System.ViewModel;


namespace Test_System.Area.Customer.Controllers
{
     [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


         ApplicationDBContext db = new();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(FilterProductVM filter , int page = 1)
        {
            var Product = db.Products.Include(e => e.Category).AsQueryable();

            // Include
            // تستخدم لما يكون عندك علاقة بين جداول وعايز تجيب بيانات الجدول المرتبط مع الجدول الأساسي في نفس الاستعلام.
            // مفيدة علشان تقلل عدد الاستعلامات على قاعدة البيانات


            // AsQueryable 
            // لو معملناش  بعض العمليات هتتنفذ على الذاكرة بعد ما تجيب كل البيانات، مش على قاعدة البيانات → ده بيبطّئ لو الداتا كبيرة.
            // بيخليك تقدر تعمل فلترة وترتيب واستعلامات مركبة قبل ما تجيب الداتا فعليًا من قاعدة البيانات.

            if (filter.name is not null)
                Product = Product.Where(e => e.Name.Contains(filter.name));   
          
            if (filter.minprice is not null)
                Product = Product.Where(e => e.Price - e.Price * e.Discount / 100 > filter.minprice);

            if (filter.maxprice is not null)
                Product = Product.Where(e => e.Price - e.Price * e.Discount / 100 < filter.minprice);

            if (filter.name is not null)
                Product = Product.Where(e => e.Name.Contains(filter.name));      

            if (filter.categotyId is not null)
                Product = Product.Where(e => e.CategoryID == filter.categotyId);

            if (filter.brandId is not null)
                Product = Product.Where(e => e.BrandID == filter.brandId);


            // ViewBag , ViewData
            // ي حاجة هتبعتها من الكنترولر للفيو 
            // ViewBag الاسهل     
            var categories = db.categories.AsEnumerable();
            ViewBag.categories = db.categories;

            var Brands = db.Brands.AsEnumerable();
            ViewBag.Brands = db.Brands;


            // ViewData
            //var categories = db.categories.AsEnumerable();
            //ViewData["categories"] = db.categories.AsEnumerable();

            //var Brands = db.Brands.AsEnumerable();
            //ViewData["Brands"] = db.Brands.AsEnumerable();



            // Pagination
            ViewBag.Totalpages = Math.Ceiling(Product.Count() / 8.0);
            ViewBag.Currentpages = page;
            Product = Product.Skip((page - 1) * 8).Take(8);

            return View(Product.ToList());
            //  Math.Ceiling
            //  يقرب أي رقم عشري لأعلى عدد صحيح
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}