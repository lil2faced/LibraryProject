


using LibraryProject.ControllerModels;

namespace LibraryProject.Interfaces
{
    public interface IUserService
    {
        Task<UserDTOChild> Get(int? id, CancellationToken cancellationToken);
        Task<List<UserDTOChild>> GetAll(CancellationToken cancellationToken);
        Task Add(UserDTOParent user, CancellationToken cancellationToken);
        Task DeleteById(int? id);
        Task Update(int? id, UserDTOChild user);
    }
}
