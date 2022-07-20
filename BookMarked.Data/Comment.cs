using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Data
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public Guid OwnerId { get; set; }

        public Review Review { get; set; }

        [Required]
        public string CommentContent { get; set; }
    }
}
