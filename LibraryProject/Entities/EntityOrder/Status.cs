using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.Entities.Orders
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public List<BookPurchaseOrder> Orders { get; set; } = new();
    }
}
