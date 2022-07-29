using BookMarked.Models.Interfaces;
using BookMarked.Models.Rating;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMarked.WebMVC.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string volumeId)
        {
            ViewData["OwnerRef"] = UserUtility.GetUserId(User);
            ViewData["VolumeRef"] = volumeId;

            return View();
        }


        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var ratings = _ratingService.GetRatings(UserUtility.GetUserId(User)); //variable 'ratings'  
            return View(ratings.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RatingCreate model)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_ratingService.CreateRating(model))
            {
                TempData["SaveResult"] = "Your rating was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Rating could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var model = _ratingService.GetRatingById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var detail = _ratingService.GetRatingById(id);
            var model = new RatingEdit()
            {
                RatingId = detail.RatingId,
                DateRead = detail.DateRead,
                Stars = detail.Stars,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RatingEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RatingId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!User.Identity.IsAuthenticated) return Unauthorized();
            if (_ratingService.UpdateRating(model))
            {
                TempData["SaveResult"] = "Your Rating was updated.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Your Rating could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var model = _ratingService.GetRatingById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();
            _ratingService.DeleteRating(id);
            TempData["SaveResult"] = "Your Rating was deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
