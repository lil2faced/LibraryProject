using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.Entities.Orders
{
    public class BookPurchaseOrder : BookPurchaseOrderWithoutExternal
    {
        [Required]
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        [Required]
        [JsonIgnore]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [JsonIgnore]
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
