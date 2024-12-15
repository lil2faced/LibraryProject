using LibraryProject.Applications;
using LibraryProject.ControllerModels;

namespace LibraryProject.Interfaces
{
    public interface IOrderService
    {
        Task Add(OrderDTOParent bookPurchaseOrder, int? StatusId, int? UserId, int? BookId, CancellationToken cancellationToken);
        Task<OrderDTOChild> GetById(int? id, CancellationToken cancellationToken);
        Task<List<OrderDTOChild>> Get(CancellationToken cancellationToken);
        Task Delete(int? id);
        Task Update(OrderDTOParent bookPurchaseOrder, int? StatusId, int? UserId, int? BookId, int? id);
    }
}
