using BookMarked.Models.Interfaces;
using Google.Apis.Books.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Services
{
    public class GoogleBooksApiClientService : IGoogleBooksApiClientService
    {
        public BooksService _booksService;

        public GoogleBooksApiClientService(IServiceProvider serviceProvider)
        {
            _booksService = (BooksService)serviceProvider.GetService(typeof(BooksService));
        }

        public void Search()
        {
            var searchResults = _booksService.Volumes.List("Percy Jackson");
            searchResults.MaxResults = 1;
            var listmodel = searchResults.Execute().Items;
        }
    }
}
