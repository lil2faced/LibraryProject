using LibraryProject.Entities.BookProps;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class BookDTOChild : BookDTOParent
    {
        [Required]
        [JsonIgnore]
        public int GenreId { get; set; }
        public GenreDTO Genre { get; set; }
        [Required]
        [JsonIgnore]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        [Required]
        [JsonIgnore]
        public int AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
        [Required]
        [JsonIgnore]
        public int SeriesId { get; set; }
        public SeriesDTO Series { get; set; }

        public List<ReviewDTOChild> Reviews { get; set; } = new();
        public List<OrderDTOChild> Orders { get; set; } = new();
        public List<LoanDTOChild> Loans { get; set; } = new();
    }
}
