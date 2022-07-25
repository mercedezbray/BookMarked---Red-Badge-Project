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
                    OwnerId = _userId,
                    VolumeId = model.VolumeId,
                    ReviewContent = model.ReviewContent,
                };
                _context.Reviews.Add(entity);
                return _context.SaveChanges() == 1;
            }

            public IList<ReviewListItem> GetReviews()
            {
                var reviews = _context.Reviews
                .Where(e => e.OwnerId == _userId)
                .Select(e =>
                    new ReviewListItem()
                    {
                        ReviewId = e.ReviewId,
                        OwnerId = _userId,
                        VolumeId=e.VolumeId,
                        RatingId = e.RatingId,
                        ReviewContent=e.ReviewContent,
                    }).ToList();
                return reviews;
            }

            public ReviewDetail GetReviewById(int Reviewid)
            {
                var rating = _context.Reviews
                    .Single(e => e.RatingId == Reviewid && e.OwnerId == _userId);
                return new ReviewDetail()
                {
                    RatingId = rating.RatingId,
                    OwnerId= _userId,
                    ReviewContent = rating.ReviewContent,
                    VolumeId = rating.VolumeId,
                    ReviewId = rating.ReviewId,
                };
            }

            public bool UpdateReview(ReviewEdit model)
            {
                var review = _context.Reviews
                    .Single(e => e.ReviewId == model.ReviewId && e.OwnerId == _userId);


                return _context.SaveChanges() == 1;
            }

            public bool DeleteReview(int reviewId)
            {
                var entity = _context.Reviews
                    .SingleOrDefault(e => e.ReviewId == reviewId && e.OwnerId == _userId);

                _context.Reviews.Remove(entity);

                return _context.SaveChanges() == 1;
            }

            public void SetUserId(Guid userId) => _userId = userId;
        }
}
