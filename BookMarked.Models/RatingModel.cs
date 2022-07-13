using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models
{
    public class RatingModel
    {
        public int RatingId { get; set; }
        public string VolumeId { get; set; }
        public int ReviewId { get; set; }
        [Required(ErrorMessage ="Rating must be a whole number between 1-10")]
        [Range(1,10)]
        public int Stars { get; set; }
        [Required(ErrorMessage ="Must enter a date in format MM/DD/YYYY")]
        [Display(Name ="Date Read")]
        public DateTime DateRead { get; set; }
    }
}
