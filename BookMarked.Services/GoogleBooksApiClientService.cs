using BookMarked.Models.Interfaces;
using Google.Apis.Books.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMarked.Models;
using Google.Apis.Books.v1.Data;

namespace BookMarked.Services
{
    public class GoogleBooksApiClientService : IGoogleBooksApiClientService
    {
        public BooksService _booksService;

        public GoogleBooksApiClientService(IServiceProvider serviceProvider)
        {
            _booksService = (BooksService)serviceProvider.GetService(typeof(BooksService));
        }

        // Gets a List of 10 book from the search term
        public IList<Volume> Search(SearchTermModel searchTermModel)
        {
            var searchResults = _booksService.Volumes.List(searchTermModel.SearchTermInput);
            searchResults.MaxResults = 10;
            return searchResults.Execute().Items;
        }
    }
}
