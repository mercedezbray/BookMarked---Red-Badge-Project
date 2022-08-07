using BookMarked.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models.Interfaces
{
    public interface ICommentService
    {
        bool CreateComment(CommentCreate model);
        IList<CommentListItem> GetComments(Guid OwnerId);
        IList<CommentListItem> GetCommentsbyReviewId(int ReviewId);
        void SetUserId(Guid userId);
        CommentDetail GetCommentById(int Commentid);
        bool UpdateComment(CommentEdit model);
        bool DeleteComment(int commentId);
    }
}
