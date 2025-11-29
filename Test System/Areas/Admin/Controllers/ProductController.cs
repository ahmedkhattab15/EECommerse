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
        Repository<Category> _CategoryRepository = new();
        Repository<Brand> _BrandRepository = new();

        // Read
        public async Task<IActionResult> Index(FilterProductVM filter, CancellationToken cancellationToken , int page = 1)
        {
            var Products = await _productRepository.GetAsync(
                includes: [e => e.Category, e => e.Brand],
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
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _CategoryRepository.GetAsync(CancellationToken : cancellationToken);
            var brands = await _BrandRepository.GetAsync(CancellationToken : cancellationToken);

            return View(new CategoriesWithBrandVM
            {
                categories = await _CategoryRepository.GetAsync(CancellationToken : cancellationToken),
                brands = await _BrandRepository.GetAsync(CancellationToken: cancellationToken)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product Product, IFormFile img , CancellationToken  cancellationToken)
        {
            if (img is not null && img.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Imeges", fileName);

                using (var stream = System.IO.File.Create(filePath))
                    img.CopyTo(stream);

                Product.MainImg = fileName;
            }
            await _productRepository.AddAsync(Product , cancellationToken); 
            await _productRepository.commitAsync( cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id , CancellationToken cancellationToken)
        {
            var Product = await _productRepository.GetOneAsync(CancellationToken: cancellationToken , tracked : false);
            if (Product is null)
                return RedirectToAction("NotFoundPadge", "Home");


            var categories = await _CategoryRepository.GetAsync(CancellationToken: cancellationToken);
            var brands = await _BrandRepository.GetAsync(CancellationToken: cancellationToken);

            return View ( new
            {
                Product = Product,
                categories = categories.AsEnumerable(),
                brands = brands.AsEnumerable()
            });
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
            await _productRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        // Delete
        public async Task<IActionResult> Delete(int id , CancellationToken cancellationToken)
        {
            var Product = await _productRepository.GetAsync(CancellationToken : cancellationToken);
            if (Product is null)
                return RedirectToAction("NotFoundPadge", "Home");

             _productRepository.Delete(Product);
            await _productRepository.commitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
