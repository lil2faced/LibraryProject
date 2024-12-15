using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ControllerModels
{
    public class UserModelWithoutExternal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
