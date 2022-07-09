using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int ReviewId { get; set; }
        public int Stars { get; set; }
        public DateTime DateRead { get; set; }
    }
}
