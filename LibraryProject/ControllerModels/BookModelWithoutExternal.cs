using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class BookModelWithoutExternal
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public DateOnly PublicationYear { get; set; }

        [Required]
        public string Publishing { get; set; }
    }
}
