using LibraryProject.Entities.Orders;

namespace LibraryProject.Interfaces
{
    public interface IStatusService
    {
        Task Add(Status status, CancellationToken cancellationToken);
        Task<List<Status>> GetAll(CancellationToken cancellationToken);
        Task<Status> GetById(int? id, CancellationToken cancellationToken);
        Task Update(int? id, Status status);
        Task Delete(int? id);
    }
}
