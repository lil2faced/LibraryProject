using LibraryProject.Entities;

namespace LibraryProject.Interfaces
{
    public interface IUserService
    {
        Task<User> Get(int? id, CancellationToken cancellationToken);
        Task<List<User>> GetAll(CancellationToken cancellationToken);
        Task Add(UserWithoutExternal user, CancellationToken cancellationToken);
        Task DeleteById(int? id);
        Task Update(int? id, User user);
    }
}
