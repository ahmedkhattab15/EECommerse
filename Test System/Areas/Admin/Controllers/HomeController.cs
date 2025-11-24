using Microsoft.AspNetCore.Mvc;

namespace Test_System.Area.Admin.Controllers
{
    [Area ("Admin")]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult NotFoundPadge()
        {
            return View();
        }
    }
}
