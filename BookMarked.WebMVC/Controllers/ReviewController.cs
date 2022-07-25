﻿using BookMarked.Models.Interfaces;
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
            _reviewService.SetUserId(userId); //Calls service and sets the userId
            return true;
        }


        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized(); //Runs SetUserIdInService method and check validity

            var reviews = _reviewService.GetReviews(); //variable 'ratings'  
            return View(reviews.ToList());
        }

        [HttpGet]
        public ActionResult Create(string VolumeId)
        {
            ViewData["VolumeId"] = VolumeId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

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
            if (!SetUserIdInService()) return Unauthorized();

            var model = _reviewService.GetReviewById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

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

            if (!SetUserIdInService()) return Unauthorized();
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
            if (!SetUserIdInService()) return Unauthorized();

            var model = _reviewService.GetReviewById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            _reviewService.DeleteReview(id);
            TempData["SaveResult"] = "Your Review was deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
