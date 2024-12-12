using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Entities.EntityBook
{
    public class BookWithoutExternal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public DateOnly PublicationYear { get; set; }
        [Required]
        public string Publishing { get; set; } = null!;
    }
}
