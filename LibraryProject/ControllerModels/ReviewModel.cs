using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class ReviewModel
    {
        public int BookId { get; set; }
        public BookModel Book { get; set; }
    }
}
