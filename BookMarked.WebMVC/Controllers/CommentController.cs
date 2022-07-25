using BookMarked.Models.Comment;
using BookMarked.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMarked.WebMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
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
            _commentService.SetUserId(userId); //Calls service and sets the userId
            return true;
        }


        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized(); //Runs SetUserIdInService method and check validity

            var comments = _commentService.GetComments(); //variable 'ratings'  
            return View(comments.ToList());
        }

        [HttpGet]
        public ActionResult Create(string VolumeId)
        {
            ViewData["VolumeId"] = VolumeId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_commentService.CreateComment(model))
            {
                TempData["SaveResult"] = "Your comment was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Comment could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _commentService.GetCommentById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var detail = _commentService.GetCommentById(id);
            var model = new CommentEdit()
            {
                CommentContent = detail.CommentContent
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ReviewId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!SetUserIdInService()) return Unauthorized();
            if (_commentService.UpdateComment(model))
            {
                TempData["SaveResult"] = "Your Comment was updated.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Your Comment could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _commentService.GetCommentById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            _commentService.DeleteComment(id);
            TempData["SaveResult"] = "Your Comment was deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
