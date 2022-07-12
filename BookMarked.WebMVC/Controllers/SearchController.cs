using Microsoft.AspNetCore.Mvc;
using BookMarked.Services;
using Google.Apis.Books.v1;
using BookMarked.Models.Interfaces;

namespace BookMarked.WebMVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private IGoogleBooksApiClientService _googleBooksApiClientService;

        public SearchController(ILogger<SearchController> logger, IGoogleBooksApiClientService googleBooksApiClientService)
        {
            _logger = logger;
            _googleBooksApiClientService = googleBooksApiClientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            _googleBooksApiClientService.Search();
            return View();
        }
    }
}
