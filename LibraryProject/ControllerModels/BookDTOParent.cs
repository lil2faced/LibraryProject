using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class BookDTOParent
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }

        public DateOnly PublicationYear { get; set; }

        [Required]
        public string Publishing { get; set; }
    }
}
