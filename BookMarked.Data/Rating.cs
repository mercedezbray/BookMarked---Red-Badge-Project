using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarked.Data
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string VolumeId { get; set; }
        [Required]
        public string VolumeTitle { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public DateTime DateRead { get; set; }
    }
}
