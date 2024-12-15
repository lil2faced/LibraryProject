using LibraryProject.Entities.EntityBook;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryProject.ControllerModels
{
    public class ReviewDTOChild : ReviewDTOParent
    {
        public int BookId { get; set; }
        [JsonIgnore]
        public BookDTOChild Book { get; set; } = new BookDTOChild();
    }
}
