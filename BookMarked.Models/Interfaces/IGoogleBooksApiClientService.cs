using Google.Apis.Books.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models.Interfaces
{
    public interface IGoogleBooksApiClientService
    {
        public IList<Volume> Search(SearchTermModel searchTermModel);
    }
}
