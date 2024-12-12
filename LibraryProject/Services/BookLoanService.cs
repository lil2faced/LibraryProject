using LibraryProject.Applications;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class BookLoanService
    {
        public static async Task<int> Add(DatabaseContext databaseContext, BookLoanWithoutExternal bookLoanWithoutExternal, int UserId, int BookId)
        {
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
        public static async Task<(int, BookLoan?)> GetById(DatabaseContext databaseContext, int id)
        {
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
        public static async Task<List<BookLoan>> Get(DatabaseContext databaseContext)
        {
            return await databaseContext.BookLoans
                .Include(b => b.User)
                .ToListAsync();
        }
        public static async Task<int> Delete(DatabaseContext databaseContext, int id)
        {
            var loan = await databaseContext.BookLoans.FindAsync(id);
            if (loan == null)
            {
                return 1;
            }
            databaseContext.BookLoans.Remove(loan);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public static async Task<int> Update(DatabaseContext databaseContext, BookLoanWithoutExternal bookLoanWithoutExternal, int UserId, int BookId, int id)
        {
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
