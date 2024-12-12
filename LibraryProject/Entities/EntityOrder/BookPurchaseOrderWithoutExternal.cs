using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Entities.Orders
{
    public class BookPurchaseOrderWithoutExternal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateOnly OrderDate { get; set; }
        
    }
}
