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
        [ForeignKey(nameof(Rating))]
        public int RatingId { get; set; }
        [Required]
        public string ReviewContent { get; set; }
    }
}
