using LibraryProject.Applications;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;
using LibraryProject.ControllerModels;
using AutoMapper;

namespace LibraryProject.Services
{
    public class BookOrderService : IOrderService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper _mapper;
        public BookOrderService(DatabaseContext databaseContext, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            _mapper = mapper;
        }
        public async Task Add(OrderDTOParent bookPurchaseOrder, int? StatusId, int? UserId, int? BookId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (bookPurchaseOrder == null || StatusId == null || UserId == null || BookId == null)
            {
                throw new ArgumentNullException();
            }
            var status = await databaseContext.Statuses.FindAsync(StatusId);
            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (status == null || book == null || user == null)
            {
                throw new Exception("Связанные параметры не найдены");
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
        }
        public async Task<OrderDTOChild> GetById(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var temp = await databaseContext.Orders
                .Include(b=> b.Status)
                .Include(b=> b.User)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (temp == null)
            {
                throw new Exception("Заявка не найдена");
            }
            return _mapper.Map<OrderDTOChild>(temp);
        }
        public async Task<List<OrderDTOChild>> Get(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await databaseContext.Orders
                .Include(b => b.Status)
                .Include(b => b.User)
                .Select(p => _mapper.Map<OrderDTOChild>(p))
                .ToListAsync();
        }
        public async Task Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var order = await databaseContext.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Заявка не найдена");
            }
            databaseContext.Orders.Remove(order);
            await databaseContext.SaveChangesAsync();
        }
        public async Task Update(OrderDTOParent bookPurchaseOrder, int? StatusId, int? UserId, int? BookId, int? id)
        {
            if (StatusId == null || bookPurchaseOrder == null || UserId == null || BookId == null || id == null)
            {
                throw new ArgumentNullException();
            }
            var temp = await databaseContext.Orders.FindAsync(id);
            if (temp == null) throw new Exception("Заявка не найдена");

            var status = await databaseContext.Statuses.FindAsync(StatusId);
            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (status == null || book == null || user == null)
            {
                throw new Exception("Связанные свойства не найдены");
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
        }
    }
}
