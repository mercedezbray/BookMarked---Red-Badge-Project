using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Data
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public string VolumeId { get; set; }
        [Required]
        public int RatingId { get; set; }
        [Required]
        public string ReviewContent { get; set; }
    }
}
