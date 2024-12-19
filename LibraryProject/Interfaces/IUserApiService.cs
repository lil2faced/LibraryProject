using LibraryProject.ControllerModels;

namespace LibraryProject.Interfaces
{
    public interface IUserApiService
    {
        Task Register(UserApiDTO user, CancellationToken token);
        Task<string> Login(UserApiDTO user, CancellationToken token);
    }
}
