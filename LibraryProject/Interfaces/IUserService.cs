using LibraryProject.Entities;

namespace LibraryProject.Interfaces
{
    public interface IUserService
    {
        Task<(int, User?)> Get(int? id);
        Task<List<User>> GetAll();
        Task<int> Add(UserWithoutExternal user);
        Task<int> DeleteById(int? id);
        Task<int> Update(int? id, User user);
    }
}
