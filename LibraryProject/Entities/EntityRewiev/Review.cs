using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.Entities.EntityRewiev
{
    public class Review : ReviewWithoutExternal
    {
        [Required]
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
    }
}
