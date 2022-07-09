﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        [Required(ErrorMessage ="Must enter text for comment")]
        [StringLength(500)]
        [Display(Name="Comment")]
        public string CommentContent { get; set; }
    }
}
