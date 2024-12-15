using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        [JsonIgnore]
        public List<BookDTOChild> Books { get; set; } = new List<BookDTOChild>();
    }
}
