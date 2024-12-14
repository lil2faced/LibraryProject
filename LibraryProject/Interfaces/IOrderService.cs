using LibraryProject.Applications;
using LibraryProject.Entities.Orders;

namespace LibraryProject.Interfaces
{
    public interface IOrderService
    {
        Task Add(BookPurchaseOrderWithoutExternal bookPurchaseOrder, int? StatusId, int? UserId, int? BookId, CancellationToken cancellationToken);
        Task<BookPurchaseOrder> GetById(int? id, CancellationToken cancellationToken);
        Task<List<BookPurchaseOrder>> Get(CancellationToken cancellationToken);
        Task Delete(int? id);
        Task Update(BookPurchaseOrderWithoutExternal bookPurchaseOrder, int? StatusId, int? UserId, int? BookId, int? id);
    }
}
