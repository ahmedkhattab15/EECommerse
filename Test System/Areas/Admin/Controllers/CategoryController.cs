using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_System.Data_Acssess;
using Test_System.Models;

namespace Test_System.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class CategoryController : Controller
    {
        ApplicationDBContext _db = new();

        // 1_ Read
        public IActionResult Index()
        {
           var brands =  _db.categories.AsNoTracking().AsQueryable();     //  عرض الداتا فقط

            return View(brands.AsEnumerable());
        }


        // 2_ Create 
        [HttpGet]   
        public IActionResult Create()
        {
           return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            _db.categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // 3_ Edit
        [HttpGet]
         public IActionResult Edit(int id)
        {
            var category = _db.categories.FirstOrDefault(e => e.id == id);
            if (category is null)
            {
                return RedirectToAction("NotFoundPadge", "Home");
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _db.categories.Update(category);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        // 4_ Delete
        public IActionResult Delete(int id)
        {
            var category = _db.categories.FirstOrDefault(e => e.id == id);
            if (category is null)
                return RedirectToAction("NotFoundPadge", "Home");

            _db.categories.Remove(category);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}