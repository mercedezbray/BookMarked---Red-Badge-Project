using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarked.Models.Rating
{
    public class RatingCreate
    {
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string VolumeId { get; set; }
        [Required]
        public string VolumeTitle { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Please enter a whole number between 1 and 10")]
        public int Stars { get; set; }
        [Required]
        public DateTime DateRead { get; set; }
    }
}