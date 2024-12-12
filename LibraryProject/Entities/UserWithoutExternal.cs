using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Entities
{
    public class UserWithoutExternal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Adress { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
    }
}
