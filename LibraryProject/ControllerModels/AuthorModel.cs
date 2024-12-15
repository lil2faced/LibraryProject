using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        [JsonIgnore]
        public List<BookModel> Books { get; set; } = new List<BookModel>();
    }
}
