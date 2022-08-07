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


        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized(); //Runs SetUserIdInService method and check validity

            var comments = _commentService.GetComments(UserUtility.GetUserId(User)); //variable 'ratings'  
            return View(comments.ToList());
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string volumeId, int ReviewId)
        {
            ViewData["OwnerRef"] = UserUtility.GetUserId(User);
            ViewData["VolumeRef"] = volumeId;
            ViewData["ReviewRef"] = ReviewId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentCreate model)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

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
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var model = _commentService.GetCommentById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

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

            if (model.CommentId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!User.Identity.IsAuthenticated) return Unauthorized();
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
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            var model = _commentService.GetCommentById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();
            _commentService.DeleteComment(id);
            TempData["SaveResult"] = "Your Comment was deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
