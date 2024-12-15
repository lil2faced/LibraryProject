using LibraryProject.ControllerModels;

namespace LibraryProject.Interfaces
{
    public interface IStatusService
    {
        Task Add(StatusDTO status, CancellationToken cancellationToken);
        Task<List<StatusDTO>> GetAll(CancellationToken cancellationToken);
        Task<StatusDTO> GetById(int? id, CancellationToken cancellationToken);
        Task Update(int? id, StatusDTO status);
        Task Delete(int? id);
    }
}
