using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        public string CommentContent { get; set; }
    }
}
