using BookMarked.Data;
using BookMarked.Models;
using BookMarked.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Services
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;
        private Guid _userId;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateRating(RatingModel model)
        {
            var entity =
                new Rating()
                {
                    OwnerId = _userId,
                    RatingId = model.RatingId,
                    VolumeId = model.VolumeId,
                    ReviewId = model.ReviewId,
                    Stars = model.Stars,
                    DateRead = model.DateRead,
                };
            _context.Ratings.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<RatingListItem> GetRatings()
        {
            var ratings = _context.Ratings
            .Where(e => e.OwnerId == _userId)
            .Select(e =>
                new RatingListItem()
                {
                    RatingId = e.RatingId,
                    VolumeId =e.VolumeId,
                    ReviewId =e.ReviewId,
                    Stars = e.Stars,
                    DateRead = e.DateRead
                }).ToList();
            return ratings;
        }

        //public RatingDetail GetRatingById(int Ratingid);
        //public bool DeleteRating(int RatingId);

        public void SetUserId(Guid userId) => _userId = userId;

    }
}
