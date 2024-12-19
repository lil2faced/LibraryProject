

using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class UserApiDTO
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
