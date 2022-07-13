using BookMarked.Models.Interfaces;
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
    }
}
