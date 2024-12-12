using LibraryProject.Applications;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class BookOrderService
    {
        public static async Task<int> Add(DatabaseContext databaseContext, BookPurchaseOrderWithoutExternal bookPurchaseOrder, int StatusId, int UserId, int BookId)
        {
            var status = await databaseContext.Statuses.FindAsync(StatusId);
            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (status == null || book == null || user == null)
            {
                return 1;
            }
            BookPurchaseOrder order = new()
            {
                OrderDate = bookPurchaseOrder.OrderDate,
                Quantity = bookPurchaseOrder.Quantity,
                Status = status,
                StatusId = status.Id,
                UserId = user.Id,
                User = user,
                Book = book,
                BookId = book.Id
            };
            await databaseContext.Orders.AddAsync(order);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public static async Task<(int, BookPurchaseOrder?)> GetById(DatabaseContext databaseContext, int id)
        {
            var temp = await databaseContext.Orders
                .Include(b=> b.Status)
                .Include(b=> b.User)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (temp == null)
            {
                return (1, null);
            }
            return (0, temp);
        }
        public static async Task<List<BookPurchaseOrder>> Get(DatabaseContext databaseContext)
        {
            return await databaseContext.Orders
                .Include(b => b.Status)
                .Include(b => b.User)
                .ToListAsync();
        }
        public static async Task<int> Delete(DatabaseContext databaseContext, int id)
        {
            var order = await databaseContext.Orders.FindAsync(id);
            if (order == null)
            {
                return 1;
            }
            databaseContext.Orders.Remove(order);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public static async Task<int> Update(DatabaseContext databaseContext, BookPurchaseOrderWithoutExternal bookPurchaseOrder, int StatusId, int UserId, int BookId, int id)
        {
            var temp = await databaseContext.Orders.FindAsync(id);
            if (temp == null) return 1;

            var status = await databaseContext.Statuses.FindAsync(StatusId);
            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (status == null || book == null || user == null)
            {
                return 1;
            }
            BookPurchaseOrder order = new()
            {
                OrderDate = bookPurchaseOrder.OrderDate,
                Quantity = bookPurchaseOrder.Quantity,
                Status = status,
                StatusId = status.Id,
                UserId = user.Id,
                User = user,
                Book = book,
                BookId = book.Id
            };
            databaseContext.Orders.Update(order);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
    }
}
