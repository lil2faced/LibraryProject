using LibraryProject.Entities.Orders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class StatusDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public List<BookPurchaseOrder> Orders { get; set; } = new List<BookPurchaseOrder>();
    }
}
