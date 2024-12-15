using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class UserDTOChild : UserDTOParent
    {
        [JsonIgnore]
        public List<OrderDTOChild> Orders { get; set; } = new List<OrderDTOChild>();
        [JsonIgnore]
        public List<LoanDTOChild> Loans { get; set; } = new List<LoanDTOChild>();
    }
}
