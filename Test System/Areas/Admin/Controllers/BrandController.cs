using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Test_System.Data_Acssess;
using Test_System.Models;
using Test_System.Repositories;

namespace Test_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        Repository<Brand> _CategoryRepository = new();


        // Read
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var brands = await _CategoryRepository.GetAsync(tracked: false);     //  عرض الداتا فقط

            return View(brands.Select(e => new
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
        public async Task<IActionResult> CreateAsync(Brand Brand, IFormFile img, CancellationToken cancellationToken)
        {
            // الصورة بتتحفظ على السيرفر واسمها في قاعدة البيانات
            // save Img in wwwroot

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                img.CopyTo(stream);
            }

            //save img in db

            Brand.Img = fileName;

            //save Brand in db


            Brand.Img = fileName;

            //save Brand in db
            await _CategoryRepository.AddAsync(Brand, cancellationToken);
            await _CategoryRepository.commitAsync(cancellationToken);

            TempData["Notification"] = "Add Brand Successfully";

            return RedirectToAction(nameof(Index));
        }
        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var Brand = await _CategoryRepository.GetOneAsync(e => e.id == id,
                CancellationToken: cancellationToken);
            if (Brand is null)
            {
                return RedirectToAction("NotFoundPadge", "Home");
            }
            return View(Brand);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(Brand Brand, IFormFile? img, CancellationToken cancellationToken)
        {
            var brandinDB = await _CategoryRepository.GetOneAsync(e => e.id == Brand.id,
                CancellationToken: cancellationToken);


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


            await _CategoryRepository.commitAsync(cancellationToken);


            TempData["Notification"] = "Update Brand Successfully";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _CategoryRepository.GetOneAsync(e => e.id == id,
                CancellationToken: cancellationToken);
            if (category is null)
                return RedirectToAction("NotFoundPadge", "Home");

            _CategoryRepository.Delete(category);
            await _CategoryRepository.commitAsync(cancellationToken);

            TempData["Notification"] = "Delete Brand Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}