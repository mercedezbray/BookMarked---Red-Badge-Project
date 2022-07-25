using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMarked.Data;
using BookMarked.Models.Comment;
using BookMarked.Models.Interfaces;

namespace BookMarked.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private Guid _userId;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity = new Comment
            {
                CommentContent = model.CommentContent,
                CommentId = model.CommentId,
                OwnerId = model.OwnerId,
                ReviewId = model.ReviewId
            };
            _context.Comments.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public IList<CommentListItem> GetComments()
        {
            var comment = _context.Comments
            .Where(e => e.OwnerId == _userId)
            .Select(e =>
                new CommentListItem()
                {
                    CommentId = e.CommentId,
                    OwnerId=e.OwnerId,
                    ReviewId = e.ReviewId,
                    CommentContent = e.CommentContent,
                }).ToList();
            return comment;
        }

        public CommentDetail GetCommentById(int Commentid)
        {
            var comment = _context.Comments
                .Single(e => e.CommentId == Commentid && e.OwnerId == _userId);
            return new CommentDetail()
            {
                CommentId=comment.CommentId,
                CommentContent=comment.CommentContent,  
                ReviewId = comment.ReviewId,
                OwnerId=comment.OwnerId,    
            };
        }

        public bool UpdateComment(CommentEdit model)
        {
            var comment = _context.Comments
                .Single(e => e.CommentId == model.CommentId && e.OwnerId == _userId);


            return _context.SaveChanges() == 1;
        }

        public bool DeleteComment(int commentId)
        {
            var entity = _context.Comments
                .SingleOrDefault(e => e.CommentId == commentId && e.OwnerId == _userId);

            _context.Comments.Remove(entity);

            return _context.SaveChanges() == 1;
        }

        public void SetUserId(Guid userId) => _userId = userId;
    }
}
