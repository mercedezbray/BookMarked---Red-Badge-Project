using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Data
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public string CommentContent { get; set; }
    }
}
