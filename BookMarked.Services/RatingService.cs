using BookMarked.Data;
using BookMarked.Models.Interfaces;
using BookMarked.Models.Rating;
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

        public bool CreateRating(RatingCreate model)
        {
            var entity = new Rating
                {
                    OwnerId = _userId,
                    VolumeId = model.VolumeId,
                    Stars = model.Stars,
                    DateRead = model.DateRead,
                };
            _context.Ratings.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public IList<RatingListItem> GetRatings()
        {
            var ratings = _context.Ratings
            .Where(e => e.OwnerId == _userId)
            .Select(e =>
                new RatingListItem()
                {
                    RatingId = e.RatingId,
                    VolumeId =e.VolumeId,
                    Stars = e.Stars,
                    DateRead = e.DateRead
                }).ToList();
            return ratings;
        }

        public RatingDetail GetRatingById(int Ratingid)
        {
            var rating = _context.Ratings
                .Single(e => e.RatingId == id && e.OwnerId == _userId);
            return new RatingDetail()
            {
                RatingId = rating.RatingId,
                VolumeId=rating.VolumeId,
                DateRead = rating.DateRead,
                Stars=rating.Stars

            };
        }
        
        public bool UpdateRating(RatingEdit model)
        {
            var rating = _context.Ratings
                .Single(e => e.RatingId == model.RatingId && e.OwnerId == _userId);
            rating.DateRead = model.DateRead;
            rating.Stars = model.Stars;

            return _context.SaveChanges() == 1;
        }

        public bool DeleteRating(int ratingId)
        {
            var entity = _context.Ratings
                .SingleOrDefault(e => e.RatingId == ratingId && e.OwnerId == _userId);

            _context.Ratings.Remove(entity);

            return _context.SaveChanges() == 1;
        }

        public void SetUserId(Guid userId) => _userId = userId;


    }
}
