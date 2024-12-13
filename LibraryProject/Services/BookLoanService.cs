using LibraryProject.Applications;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class BookLoanService : ILoanService
    {
        private readonly DatabaseContext databaseContext;
        public BookLoanService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<int> Add(BookLoanWithoutExternal bookLoanWithoutExternal, int? UserId, int? BookId)
        {
            if (bookLoanWithoutExternal == null || UserId == null || BookId == null)
            {
                throw new ArgumentNullException();
            }
            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (book == null || user == null)
            {
                return 1;
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
            return 0;
        }
        public async Task<(int, BookLoan?)> GetById(int? id)
        {
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
                return (1, null);
            }
            return (0, temp);
        }
        public async Task<List<BookLoan>> Get()
        {
            return await databaseContext.BookLoans
                .Include(b => b.User)
                .ToListAsync();
        }
        public async Task<int> Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var loan = await databaseContext.BookLoans.FindAsync(id);
            if (loan == null)
            {
                return 1;
            }
            databaseContext.BookLoans.Remove(loan);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public async Task<int> Update(BookLoanWithoutExternal bookLoanWithoutExternal, int? UserId, int? BookId, int? id)
        {
            if (UserId == null || BookId == null || id == null || bookLoanWithoutExternal == null)
            {
                throw new ArgumentNullException();
            }
            var temp = await databaseContext.BookLoans.FindAsync(id);
            if (temp == null) return 1;

            var book = await databaseContext.Books.FindAsync(BookId);
            var user = await databaseContext.Users.FindAsync(UserId);
            if (book == null || user == null)
            {
                return 1;
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
            return 0;
        }
    }
}
