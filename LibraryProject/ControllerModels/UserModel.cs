using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class UserModel
    {
        public List<OrderModel> Orders { get; set; }
        public List<LoanModel> Loans { get; set; }
    }
}
