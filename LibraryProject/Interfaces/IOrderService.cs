using LibraryProject.Applications;
using LibraryProject.Entities.Orders;

namespace LibraryProject.Interfaces
{
    public interface IOrderService
    {
        Task<int> Add(BookPurchaseOrderWithoutExternal bookPurchaseOrder, int? StatusId, int? UserId, int? BookId);
        Task<(int, BookPurchaseOrder?)> GetById(int? id);
        Task<List<BookPurchaseOrder>> Get();
        Task<int> Delete(int? id);
        Task<int> Update(BookPurchaseOrderWithoutExternal bookPurchaseOrder, int? StatusId, int? UserId, int? BookId, int? id);
    }
}
