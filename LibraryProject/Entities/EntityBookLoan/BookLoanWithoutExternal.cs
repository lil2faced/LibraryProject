using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Entities.EntityBookLoan
{
    public class BookLoanWithoutExternal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateOnly DateLoan { get; set; }
        public DateOnly DateReturn { get; set; }
    }
}
