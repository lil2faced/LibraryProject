using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class LoanDTOParent
    {
        public int Id { get; set; }
        [Required]
        public DateOnly DateLoan { get; set; }
        [Required]
        public DateOnly DateReturn { get; set; }
    }
}
