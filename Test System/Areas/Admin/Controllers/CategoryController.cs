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
    public class CategoryController : Controller
    {
        Repository<Category> _CategoryRepository = new Repository<Category>();

        // 1_ Read
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _CategoryRepository.GetAsync(
             includes: [e => e.name], tracked: false, CancellationToken: cancellationToken);

            return View(categories.AsEnumerable());
        }
        // 2_ Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _CategoryRepository.AddAsync(category, cancellationToken);
            await _CategoryRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }
        // 3_ Edit
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _CategoryRepository.GetOneAsync(e => e.id == id,
                CancellationToken: cancellationToken);
            if (category is null)
            {
                return RedirectToAction("NotFoundPadge", "Home");
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(Category category, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _CategoryRepository.Update(category);
            await _CategoryRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }
        // 4_ Delete
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _CategoryRepository.GetOneAsync(e => e.id == id,
                CancellationToken: cancellationToken);
            if (category is null)
                return RedirectToAction("NotFoundPadge", "Home");

            _CategoryRepository.Delete(category);
            await _CategoryRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }
    }
}