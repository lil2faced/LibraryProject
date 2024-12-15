using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public List<BookDTOChild> Books { get; set; } = new List<BookDTOChild>();
    }
}
