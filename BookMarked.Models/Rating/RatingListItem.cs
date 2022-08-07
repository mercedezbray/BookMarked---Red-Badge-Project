using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models.Rating
{
    public class RatingListItem
    {
        public int RatingId { get; set; }
        public string VolumeId { get; set; }
        [Required]
        public string VolumeTitle { get; set; }
        public int Stars { get; set; }
        [Display(Name = "Date Read")]
        public DateTime DateRead { get; set; }

    }
}