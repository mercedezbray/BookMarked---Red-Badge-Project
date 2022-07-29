using BookMarked.Models.Interfaces;
using BookMarked.Models.Review;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMarked.WebMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
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
            if (!User.Identity.IsAuthenticated) return Unauthorized(); //Runs SetUserIdInService method and check validity

            var reviews = _reviewService.GetReviews(); //variable 'ratings'  
            return View(reviews.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreate model)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_reviewService.CreateReview(model))
            {
                TempData["SaveResult"] = "Your review was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Review could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var model = _reviewService.GetReviewById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var detail = _reviewService.GetReviewById(id);
            var model = new ReviewEdit()
            {
                ReviewContent = detail.ReviewContent
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReviewEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ReviewId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!User.Identity.IsAuthenticated) return Unauthorized();
            if (_reviewService.UpdateReview(model))
            {
                TempData["SaveResult"] = "Your Review was updated.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Your Review could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var model = _reviewService.GetReviewById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();
            _reviewService.DeleteReview(id);
            TempData["SaveResult"] = "Your Review was deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
