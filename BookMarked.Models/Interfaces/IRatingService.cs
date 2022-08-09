using BookMarked.Models.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models.Interfaces
{
    public interface IRatingService
    {
        bool CreateRating(RatingCreate model);
        public IList<RatingListItem> GetRatings();
        public RatingDetail GetRatingById(int Ratingid);
        bool UpdateRating(RatingEdit model);
        bool DeleteRating(int ratingId);
    }
}
