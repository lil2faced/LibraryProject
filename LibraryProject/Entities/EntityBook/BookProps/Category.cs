using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryProject.Entities.EntityBook;

namespace LibraryProject.Entities.BookProps
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public List<Book> Books { get; set; } = new();
    }
}
