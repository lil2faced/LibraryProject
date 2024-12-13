using LibraryProject.Applications;

using LibraryProject.Entities.EntityBook;
using LibraryProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace LibraryProject.Services
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _db;
        public BookService(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }
        public async Task AddBookAsync(int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (GenreId == null || CategoryId == null || AuthorId == null || SeriesId == null || WithoutExternalBook == null)
            {
                throw new ArgumentNullException("Аргументы равны null");
            }
            var genre = await _db.Genres.FindAsync(GenreId);
            var category = await _db.Categories.FindAsync(CategoryId);
            var author = await _db.BookAuthors.FindAsync(AuthorId);
            var series = await _db.BookSeries.FindAsync(SeriesId);

            var bookTemp = await _db.Books.Where(b => b.Name == WithoutExternalBook.Name).FirstOrDefaultAsync();

            if (genre == null || category == null || author == null || series == null)
            {
                throw new Exception("Связанные свойства не найдены");
            }
            else if (bookTemp != null)
            {
                throw new Exception("Книга не найдена");
            }

            Book book = new Book()
            {
                Name = WithoutExternalBook.Name,
                Genre = genre,
                Category = category,
                GenreId = (int)GenreId,
                CategoryId = (int)CategoryId,
                AuthorId = (int)AuthorId,
                SeriesId = (int)SeriesId,
                PublicationYear = WithoutExternalBook.PublicationYear,
                Publishing = WithoutExternalBook.Publishing,
                Price = WithoutExternalBook.Price,
                Author = author,
                Series = series
            };

            _db.Books.Add(book);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.Books.Include(b => b.Genre).Include(b => b.Category).Include(b => b.Series).Include(b => b.Author).Include(b => b.Reviews).ToListAsync();
        }
        public async Task<Book> ReturnBookByIdAsync(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException("Аргумент равен null");
            }
            var book = await _db.Books.Include(b => b.Genre).Include(b => b.Category).Include(b => b.Series).Include(b => b.Author).Include(b => b.Reviews).Where(b => b.Id == id).FirstOrDefaultAsync();
            if (book == null)
            {
                throw new Exception("Книга не найдена");
            }
            return book;
        }
        public async Task DeleteByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Арумент равен Null");
            }
            var book1 = await _db.Books.FindAsync(id);
            if (book1 == null)
            {
                throw new Exception("Книга не найдена");
            }
            _db.Books.Remove(book1);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateByIDAsync(int? id, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook)
        {
            if (id == null || GenreId == null || CategoryId == null || AuthorId == null || SeriesId == null || WithoutExternalBook == null)
            {
                throw new ArgumentNullException();
            }
            var book = await _db.Books.FindAsync(id);
            if (book == null) throw new Exception("Книга не найдена"); 

            var genre = await _db.Genres.FindAsync(GenreId);
            var category = await _db.Categories.FindAsync(CategoryId);
            var author = await _db.BookAuthors.FindAsync(AuthorId);
            var series = await _db.BookSeries.FindAsync(SeriesId);
            if (genre == null || category == null || author == null || series == null)
            {
                throw new Exception("Связанные свойства не найдены");
            }
            book.Name = WithoutExternalBook.Name;
            book.Genre = genre;
            book.Category = category;
            book.GenreId = (int)GenreId;
            book.CategoryId = (int)CategoryId;
            book.AuthorId = (int)AuthorId;
            book.SeriesId = (int)SeriesId;
            book.PublicationYear = WithoutExternalBook.PublicationYear;
            book.Publishing = WithoutExternalBook.Publishing;
            book.Price = WithoutExternalBook.Price;
            book.Author = author;
            book.Series = series;
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }
    }
}
