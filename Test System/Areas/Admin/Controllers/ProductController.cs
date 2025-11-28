using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Test_System.Data_Acssess;
using Test_System.Models;
using Test_System.Repositorie;
using Test_System.Repositories;
using Test_System.ViewModel;

namespace Test_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        ProductRepository _productRepository = new();
        CategoryRepository<Category> _CategoryRepository = new();
        CategoryRepository<Brand> _BrandRepository = new();

        // Read
        public async Task<IActionResult> Index(FilterProductVM filter, CancellationToken cancellationToken)
        {
            var Products = await _productRepository.GetAsync(
                includes: [(e => e.Category), (e => e.Brand)],
                tracked: false,
                cancellationToken: cancellationToken);

            if (!string.IsNullOrEmpty(filter.name))
                Products = Products.Where(e => e.Name.Contains(filter.name));

            if (filter.minprice is not null)
                Products = Products.Where(e =>
                    e.Price - (e.Price * e.Discount / 100) >= filter.minprice);

            if (filter.maxprice is not null)
                Products = Products.Where(e =>
                    e.Price - (e.Price * e.Discount / 100) <= filter.maxprice);

            if (filter.categotyId is not null)
                Products = Products.Where(e => e.CategoryID == filter.categotyId);

            if (filter.brandId is not null)
                Products = Products.Where(e => e.BrandID == filter.brandId);

            ViewBag.categories = await _CategoryRepository.GetAsync();
            ViewBag.Brands = await _BrandRepository.GetAsync();

            return View(Products.ToList());
        }

        // Create 
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CategoriesWithBrandVM
            {
                categories = await _CategoryRepository.GetAsync(),
                brands = await _BrandRepository.GetAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product Product, IFormFile img)
        {
            if (img is not null && img.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);

                using (var stream = System.IO.File.Create(filePath))
                    img.CopyTo(stream);

                Product.MainImg = fileName;
            }

            await _productRepository.AddAsync(Product); 
            return RedirectToAction(nameof(Index));
        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Product = await _productRepository.GetAsync();
            if (Product is null)
                return RedirectToAction("NotFoundPadge", "Home");

            return View(Product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product Product, IFormFile? img , CancellationToken cancellationToken)
        {
            var ProductinDB = await _productRepository.GetOneAsync(
            e => e.Id == Product.Id,
            tracked: true,
            cancellationToken: cancellationToken
);
            if (ProductinDB is null)
                return RedirectToAction("NotFoundPadge", "Home");

            if (img is not null && img.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);

                using (var stream = System.IO.File.Create(filePath))
                    img.CopyTo(stream);
    
                Product.MainImg = fileName;
            }
            else
            {
                Product.MainImg = ProductinDB.MainImg;
            }

             _productRepository.Update(Product); 
            return RedirectToAction(nameof(Index));
        }

        // Delete
        public async Task<IActionResult> Delete(int id , CancellationToken cancellationToken)
        {
            var Product = await _productRepository.GetAsync(cancellationToken);
            if (Product is null)
                return RedirectToAction("NotFoundPadge", "Home");

             _productRepository.Delete(Product);
            return RedirectToAction(nameof(Index));
        }
    }
}
