using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.Entities.EntityBookLoan
{
    public class BookLoan : BookLoanWithoutExternal
    {
        [Required]
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; } = new Book();
        [Required]
        [JsonIgnore]
        public int UserId { get; set; }
        public User User { get; set; } = new User();
    }
}
