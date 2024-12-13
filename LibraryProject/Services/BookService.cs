using LibraryProject.Applications;

using LibraryProject.Entities.EntityBook;
using LibraryProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _db;
        public BookService(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }
        public async Task<int> AddBookAsync(int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook)
        {
            if (GenreId == null || CategoryId == null || AuthorId == null || SeriesId == null || WithoutExternalBook == null)
            {
                throw new ArgumentNullException();
            }
            var genre = await _db.Genres.FindAsync(GenreId);
            var category = await _db.Categories.FindAsync(CategoryId);
            var author = await _db.BookAuthors.FindAsync(AuthorId);
            var series = await _db.BookSeries.FindAsync(SeriesId);

            var bookTemp = await _db.Books.Where(b => b.Name == WithoutExternalBook.Name).FirstOrDefaultAsync();

            if (genre == null || category == null || author == null || series == null)
            {
                return 1; 
            }
            else if (bookTemp != null)
            {
                return 2;
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

            return 0;
        }
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _db.Books.Include(b => b.Genre).Include(b => b.Category).Include(b => b.Series).Include(b => b.Author).Include(b => b.Reviews).ToListAsync();
        }
        public async Task<(int, Book?)> ReturnBookByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var book = await _db.Books.Include(b => b.Genre).Include(b => b.Category).Include(b => b.Series).Include(b => b.Author).Include(b => b.Reviews).Where(b => b.Id == id).FirstOrDefaultAsync();
            if (book == null)
            {
                return (1, book);
            }
            return (0, book);
        }
        public async Task<int> DeleteByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var book1 = await _db.Books.FindAsync(id);
            if (book1 == null)
            {
                return 1;
            }
            _db.Books.Remove(book1);
            await _db.SaveChangesAsync();
            return 0;
        }
        public async Task<int> UpdateByIDAsync(int? id, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook)
        {
            if (id == null || GenreId == null || CategoryId == null || AuthorId == null || SeriesId == null || WithoutExternalBook == null)
            {
                throw new ArgumentNullException();
            }
            var book = await _db.Books.FindAsync(id);
            if (book == null) return 1; 

            var genre = await _db.Genres.FindAsync(GenreId);
            var category = await _db.Categories.FindAsync(CategoryId);
            var author = await _db.BookAuthors.FindAsync(AuthorId);
            var series = await _db.BookSeries.FindAsync(SeriesId);
            if (genre == null || category == null || author == null || series == null)
            {
                return 2; 
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

            return 0; 
        }
    }
}
