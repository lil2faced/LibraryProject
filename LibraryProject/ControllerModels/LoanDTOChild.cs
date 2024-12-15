using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class LoanDTOChild : LoanDTOParent
    {
        public int BookId { get; set; }
        [JsonIgnore]
        public BookDTOChild Book { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public UserDTOChild User { get; set; }
    }
}
