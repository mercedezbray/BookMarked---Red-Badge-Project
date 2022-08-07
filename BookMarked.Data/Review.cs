using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Data
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public Guid OwnerId { get; set; }

        [Required]
        public string VolumeId { get; set; }
        [Required]
        public string VolumeTitle { get; set; }

        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        [Required]
        public string ReviewContent { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
