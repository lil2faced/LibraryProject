using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class ReviewModelWithoutExternal
    {
        public int Id { get; set; }
        public string BodyReview { get; set; }
        public DateOnly DateReview { get; set; }
    }
}
