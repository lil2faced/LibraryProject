using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Entities.EntityRewiev
{
    public class ReviewWithoutExternal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BodyReview { get; set; } = null!;
        [Required]
        public DateOnly DateReview { get; set; }
    }
}
