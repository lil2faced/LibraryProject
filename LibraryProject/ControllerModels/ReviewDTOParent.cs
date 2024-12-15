using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class ReviewDTOParent
    {
        public int Id { get; set; }
        [Required]
        public string BodyReview { get; set; } = string.Empty;
        [Required]
        public DateOnly DateReview { get; set; }
    }
}
