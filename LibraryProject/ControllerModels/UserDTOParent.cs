using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class UserDTOParent
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
