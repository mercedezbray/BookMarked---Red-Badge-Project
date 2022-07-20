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

        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value; //returns a string
            if (userIdClaim == null) return default; //make sure string is not null
            return Guid.Parse(userIdClaim); //parsing string into a guid
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId(); // Calls up to get the userId 
            if (userId == null) return false; //Make sure the userId is not null
            _ratingService.SetUserId(userId); //Calls service and sets the userId
            return true;
        }


        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized(); //Runs SetUserIdInService method and check validity

            var ratings = _ratingService.GetRatings(); //variable 'ratings'  
            return View(ratings.ToList());
        }

        [HttpGet]
        public ActionResult Create(string VolumeId)
        {
            ViewData["VolumeId"] = VolumeId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RatingCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

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
            if (!SetUserIdInService()) return Unauthorized();

            var model = _ratingService.GetRatingById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

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

            if (!SetUserIdInService()) return Unauthorized();
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
            if (!SetUserIdInService()) return Unauthorized();

            var model = _ratingService.GetRatingById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            _ratingService.DeleteRating(id);
            TempData["SaveResult"] = "Your Rating was deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
