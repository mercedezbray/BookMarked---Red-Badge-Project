using Microsoft.AspNetCore.Mvc;

namespace BookMarked.WebMVC.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
