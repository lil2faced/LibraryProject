using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class LoanModelWithoutExternal
    {
        public int Id { get; set; }
        public DateOnly DateLoan { get; set; }
        public DateOnly DateReturn { get; set; }
    }
}
