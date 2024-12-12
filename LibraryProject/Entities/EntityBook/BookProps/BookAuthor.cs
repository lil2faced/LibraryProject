using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryProject.Entities.EntityBook;

namespace LibraryProject.Entities.BookProps
{
    public class BookAuthor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Biography { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Book> Books { get; set; } = new();
    }
}
