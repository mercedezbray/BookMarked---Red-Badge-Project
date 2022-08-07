using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models.Review
{
    public class ReviewListItem
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public string VolumeId { get; set; }
        [Required]
        public string VolumeTitle { get; set; }
        [Required]
        public int RatingId { get; set; }
        [Required(ErrorMessage = "Must enter text for a review")]
        [Display(Name = "Review")]
        [StringLength(1000)]
        public string ReviewContent { get; set; }

    }
}
