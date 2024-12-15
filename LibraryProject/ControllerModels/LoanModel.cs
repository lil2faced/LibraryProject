using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class LoanModel
    {
        public int BookId { get; set; }
        public BookModel Book { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
