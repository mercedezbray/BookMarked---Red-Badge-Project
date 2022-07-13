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
        IEnumerable<RatingListItem> GetRatings();
        void SetUserId (Guid userId);
        //RatingDetail GetRatingbyId(int id);
        //bool UpdateRating(RatingEdit model);
        //IEnumerable<RatingListItem> CreateRatingDropDownList();
    }
}
