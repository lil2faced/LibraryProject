using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class OrderDTOParent
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateOnly OrderDate { get; set; }
    }
}
