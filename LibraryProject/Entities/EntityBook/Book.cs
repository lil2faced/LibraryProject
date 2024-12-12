using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.EntityRewiev;
using LibraryProject.Entities.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryProject.Entities.EntityBook
{
    public class Book : BookWithoutExternal
    {
        [Required]
        [JsonIgnore]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        [Required]
        [JsonIgnore]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [JsonIgnore]
        public int AuthorId { get; set; }
        public BookAuthor Author { get; set; } = null!;
        [Required]
        [JsonIgnore]
        public int SeriesId { get; set; }
        public Series Series { get; set; } = new Series();
        public List<Review> Reviews { get; set; } = new();
        public List<BookPurchaseOrder> Orders { get; set; } = new();
        public List<BookLoan> Loans { get; set; } = new();

    }
}
