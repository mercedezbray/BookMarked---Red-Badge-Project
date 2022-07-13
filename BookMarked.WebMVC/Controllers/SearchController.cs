using BookMarked.Models;
using BookMarked.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IActionResult SearchPage()
        {
            return View();
        }

        // Takes the Search Terms and runs it tthrough the GoogleBooksApi
        [HttpGet]
        public IActionResult SearchWithTerms(SearchTermModel searchTermModel)
        {
            var results = _googleBooksApiClientService.Search(searchTermModel);
            return View(results);
        }
    }
    
}
