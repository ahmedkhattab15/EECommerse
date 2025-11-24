using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_System.Data_Acssess;
using Test_System.Models;
using Test_System.ViewModel;

namespace Test_System.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class ProductController : Controller
    {
        ApplicationDBContext _db = new();

        // Read
        public IActionResult Index(FilterProductVM filter)
        {
           var Products =  _db.Products.AsNoTracking().AsQueryable();     //  عرض الداتا فقط


            Products = Products.Include(e => e.Category).Include(b=>b.Brand);



            Products = Products.Include(e => e.Category).Include(b=>b.Brand);



            var Product = _db.Products.Include(e => e.Category).AsQueryable();


            if (filter.name is not null)

                Product = Product.Where(e => e.Name.Contains(filter.name)); 
               
            if (filter.minprice is not null)
                Product = Product.Where(e => e.Price - e.Price * e.Discount / 100 > filter.minprice);


            if (filter.maxprice is not null)
                Product = Product.Where(e => e.Price - e.Price * e.Discount / 100 < filter.minprice);

            if (filter.maxprice is not null)
                Product = Product.Where(e => e.Price - e.Price * e.Discount / 100 < filter.minprice);

            if (filter.categotyId is not null)
                Product = Product.Where(e => e.CategoryID == filter.categotyId);

            if (filter.brandId is not null)
                Product = Product.Where(e => e.BrandID == filter.brandId);


            var categories = _db.categories.AsEnumerable();
            ViewBag.categories = _db.categories;

            var Brands = _db.Brands.AsEnumerable();
            ViewBag.Brands = _db.Brands;

            return View(Products.AsEnumerable());
        }

        // Create 

        [HttpGet]
        public IActionResult Create()
        {

            var categories = _db.categories.AsEnumerable();
            var Brands = _db.Brands.AsEnumerable();

            return View(new CategoriesWithBrandVM
            {
                categories = categories.AsEnumerable(),
                brands = Brands.AsEnumerable()
            });
        }

        [HttpPost]
        public IActionResult Create(Product Product , IFormFile img)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);
            // save Img in wwwroot

            using(var stream = System.IO.File.Create(filePath))
            {
                img.CopyTo(stream);
            }


            Product.MainImg = fileName;


            //save img in db

            Product.MainImg = fileName;

            //save Product in db


            _db.Products.Add(Product);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }
        // Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Product = _db.Products.FirstOrDefault(e => e.Id == id);
            if (Product is null)
            {
                return RedirectToAction("NotFoundPadge", "Home");
            }   
            return View(Product);
        }

        [HttpPost]
        public IActionResult Edit(Product Product  , IFormFile? img)
        {
           var ProductinDB = _db.Products.FirstOrDefault(e => e.Id == Product.Id);

            if (ProductinDB is null) 
                return RedirectToAction("NotFoundPadge", "Home");

            if (img is not null)
            {

                if(img.Length>0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        img.CopyTo(stream);
                    }

                    Product.MainImg = fileName;


                    Product.MainImg = fileName;


                }
            }
            else
            {
                Product.MainImg = ProductinDB.MainImg;
            }
            _db.Products.Update(Product);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
             
        }

        // Delete

        public IActionResult Delete(int id)
        {
            var Product = _db.Products.FirstOrDefault(e => e.Id == id);
            if (Product is null)
                return RedirectToAction("NotFoundPadge", "Home");

            _db.Products.Remove(Product);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}