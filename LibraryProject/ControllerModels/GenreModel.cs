using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class GenreModel
    {
        public int Id { get; set; }
        [Required]  
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public List<BookModel> Books { get; set; } = new List<BookModel>();
    }
}
