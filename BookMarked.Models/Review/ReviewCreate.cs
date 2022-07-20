using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Models.Review
{
    public class ReviewCreate
    {
        [Key]
        public int RatingId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string VolumeId { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public DateTime DateRead { get; set; }
    }
}
