using BookMarked.Data;
using BookMarked.Models.Interfaces;
using BookMarked.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Services
{
        public class ReviewService : IReviewService
        {
            private readonly ApplicationDbContext _context;
            private Guid _userId;

            public ReviewService(ApplicationDbContext context)
            {
                _context = context;
            }

            public bool CreateReview(ReviewCreate model)
            {
                var entity = new Review
                {
                    OwnerId = model.OwnerId,
                    VolumeId = model.VolumeId,
                    VolumeTitle = model.VolumeTitle,
                    RatingId = model.RatingId,
                    ReviewContent = model.ReviewContent,
                };
                _context.Reviews.Add(entity);
                return _context.SaveChanges() == 1;
            }

            public IList<ReviewListItem> GetReviews(Guid OwnerId)
            {
                var reviews = _context.Reviews
                .Where(e => e.OwnerId == OwnerId)
                .Select(e =>
                    new ReviewListItem()
                    {
                        ReviewId = e.ReviewId,
                        VolumeId=e.VolumeId,
                        VolumeTitle=e.VolumeTitle,
                        RatingId = e.RatingId,
                        ReviewContent=e.ReviewContent,
                    }).ToList();
                return reviews;
            }

            public ReviewDetail GetReviewById(int ReviewId)
            {
                var review = _context.Reviews
                    .Single(e => e.ReviewId == ReviewId);
                return new ReviewDetail()
                {
                    RatingId = review.RatingId,
                    OwnerId= review.OwnerId,
                    ReviewContent = review.ReviewContent,
                    VolumeId = review.VolumeId,
                    VolumeTitle = review.VolumeTitle,
                    ReviewId = review.ReviewId,
                };
            }


            public bool UpdateReview(ReviewEdit model)
            {
            var review = _context.Reviews
                .Single(e => e.ReviewId == model.ReviewId);
            review.ReviewContent = model.ReviewContent;

                return _context.SaveChanges() == 1;
            }

            public bool DeleteReview(int reviewId)
            {
                var entity = _context.Reviews
                    .SingleOrDefault(e => e.ReviewId == reviewId);

                _context.Reviews.Remove(entity);

                return _context.SaveChanges() == 1;
            }

            public void SetUserId(Guid userId) => _userId = userId;
        }
}
