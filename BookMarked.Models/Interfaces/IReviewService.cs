using BookMarked.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models.Interfaces
{
    public interface IReviewService
    {
        bool CreateReview(ReviewCreate model);
        IList<ReviewListItem> GetReviews(Guid OwnerId);
        void SetUserId(Guid userId);
        ReviewDetail GetReviewById(int Reviewid);
        bool UpdateReview(ReviewEdit model);
        bool DeleteReview(int reviewId);
    }
}
