using LibraryProject.Entities.Orders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookPurchaseOrder> Orders { get; set; }
    }
}
