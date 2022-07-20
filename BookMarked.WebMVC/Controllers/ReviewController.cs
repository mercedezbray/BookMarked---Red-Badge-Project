using Microsoft.AspNetCore.Mvc;

namespace BookMarked.WebMVC.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
