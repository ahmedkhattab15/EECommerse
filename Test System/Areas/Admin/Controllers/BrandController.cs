using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_System.Data_Acssess;
using Test_System.Models;

namespace Test_System.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class BrandController : Controller
    {
        ApplicationDBContext _db = new();

        // Read
        public IActionResult Index()
        {
           var brands =  _db.Brands.AsNoTracking().AsQueryable();     //  عرض الداتا فقط

            return View(brands.Select(e=>new
            {
                e.id,
                e.name,
                e.description,
                e.Status,
                e.Img
            }).AsEnumerable());
        }

        // Create 

        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        public IActionResult Create(Brand Brand , IFormFile img)
        {
            // الصورة بتتحفظ على السيرفر واسمها في قاعدة البيانات
            // save Img in wwwroot

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);

            using(var stream = System.IO.File.Create(filePath))
            {
                img.CopyTo(stream);
            }

            //save img in db

            Brand.Img = fileName;

            //save Brand in db


            Brand.Img = fileName;

            //save Brand in db
            _db.Brands.Add(Brand);
            _db.SaveChanges();

            TempData["Notification"] = "Add Brand Successfully";

            return RedirectToAction(nameof(Index));
        }
        // Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Brand = _db.Brands.FirstOrDefault(e => e.id == id);
            if (Brand is null)
            {
                return RedirectToAction("NotFoundPadge", "Home");
            }
            return View(Brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand Brand, IFormFile? img)
        {
            var brandinDB = _db.Brands.FirstOrDefault(e => e.id == Brand.id);

            if (brandinDB is null)
                return RedirectToAction("NotFoundPadge", "Home");

            if (img is not null && img.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    img.CopyTo(stream);
                }

                brandinDB.Img = fileName;
            }
            else
            {
                brandinDB.Img = brandinDB.Img;
            }

        
            brandinDB.name = Brand.name;
            brandinDB.description = Brand.description;
           

            _db.SaveChanges();

            TempData["Notification"] = "Update Brand Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var Brand = _db.Brands.FirstOrDefault(e => e.id == id);
            if (Brand is null)
                return RedirectToAction("NotFoundPadge", "Home");

            _db.Brands.Remove(Brand);
            _db.SaveChanges();

            TempData["Notification"] = "Delete Brand Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}