using LibraryProject.Entities.Orders;

namespace LibraryProject.Interfaces
{
    public interface IStatusService
    {
        Task<int> Add(Status status);
        Task<List<Status>> GetAll();
        Task<(int, Status?)> GetById(int? id);
        Task<int> Update(int? id, Status status);
        Task<int> Delete(int? id);
    }
}
