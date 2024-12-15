using LibraryProject.Applications;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;
using LibraryProject.ControllerModels;
using AutoMapper;

namespace LibraryProject.Services
{
    public class BookLoanService : ILoanService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper _mapper;
        public BookLoanService(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            this.databaseContext = databaseContext;
        }
        public async Task Add(LoanDTOParent bookLoanWithoutExternal, int? UserId, int? BookId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (bookLoanWithoutExternal == null || UserId == null || BookId == null)
            {
                throw new ArgumentNullException();
            }
            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (book == null || user == null)
            {
                throw new Exception("Связанные свойства не найдены");
            }
            BookLoan bookLoan = new()
            {
                DateLoan = bookLoanWithoutExternal.DateLoan,
                DateReturn = bookLoanWithoutExternal.DateReturn,
                UserId = user.Id,
                BookId = book.Id,
                Book = book,
                User = user
            };
            await databaseContext.BookLoans.AddAsync(bookLoan);
            await databaseContext.SaveChangesAsync();
        }
        public async Task<LoanDTOChild> GetById(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var temp = await databaseContext.BookLoans
                .Include(b => b.User)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (temp == null)
            {
                throw new Exception("Запись не найдена");
            }
            return _mapper.Map<LoanDTOChild>(temp);
        }
        public async Task<List<LoanDTOChild>> Get(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await databaseContext.BookLoans
                .Include(b => b.User)
                .Select(p => _mapper.Map<LoanDTOChild>(p))
                .ToListAsync();
        }
        public async Task Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var loan = await databaseContext.BookLoans.FindAsync(id);
            if (loan == null)
            {
                throw new Exception("Запись не найдена");
            }
            databaseContext.BookLoans.Remove(loan);
            await databaseContext.SaveChangesAsync();
        }
        public async Task Update(LoanDTOParent bookLoanWithoutExternal, int? UserId, int? BookId, int? id)
        {
            if (UserId == null || BookId == null || id == null || bookLoanWithoutExternal == null)
            {
                throw new ArgumentNullException();
            }
            var temp = await databaseContext.BookLoans.FindAsync(id);
            if (temp == null) throw new Exception("Запись не найдена");

            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (book == null || user == null)
            {
                throw new Exception("Связанные свойства не найдены");
            }
            BookLoan loan = new()
            {
                DateLoan = bookLoanWithoutExternal.DateLoan,
                DateReturn = bookLoanWithoutExternal.DateReturn,
                UserId = user.Id,
                User = user,
                Book = book,
                BookId = book.Id
            };
            databaseContext.BookLoans.Update(loan);
            await databaseContext.SaveChangesAsync();
        }
    }
}
