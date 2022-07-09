using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int RatingId { get; set; }
        public string ReviewContent { get; set; }
    }
}
