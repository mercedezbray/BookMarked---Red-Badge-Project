using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Data
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public DateTime DateRead { get; set; }
    }
}
