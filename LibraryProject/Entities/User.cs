using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.Entities
{
    public class User : UserWithoutExternal
    {
        [JsonIgnore]
        public List<BookPurchaseOrder> Orders { get; set; }
        [JsonIgnore]
        public List<BookLoan> Loans { get; set; }
    }
}
