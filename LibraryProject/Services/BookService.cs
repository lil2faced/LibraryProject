using LibraryProject.Applications;

using LibraryProject.Entities.EntityBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class BookService
    {
        /// <summary>
        /// Добавляет книгу в базу данных
        /// </summary>
        /// <param name="_db"> контекст базы данных</param>
        /// <param name="GenreId"> Айди жанра </param>
        /// <param name="CategoryId"> Айди категории </param>
        /// <param name="AuthorId"> Айди автора </param>
        /// <param name="SeriesId"> Айди серии книг</param>
        /// <param name="WithoutExternalBook"> Обьект книги без внешних связей</param>
        /// <returns>Возвращает 1 если жанр, категория, автор или серия книг не найдены
        /// Возвращает 2 если книга с таким названием уже есть
        /// Возвращает 0 если ошибок не произошло
        /// </returns>
        public static async Task<int> AddBookAsync(DatabaseContext _db, int GenreId, int CategoryId, int AuthorId, int SeriesId, BookWithoutExternal WithoutExternalBook)
        {
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
                GenreId = GenreId,
                CategoryId = CategoryId,
                AuthorId = AuthorId,
                SeriesId = SeriesId,
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
        /// <summary>
        /// Возвращает список всех книг из базы данных вместе с связанными с ними данными
        /// </summary>
        /// <param name="_db"> Контекст базы данных</param>
        /// <returns>List<Book></returns>
        static public async Task<List<Book>> GetAllBooksAsync(DatabaseContext _db)
        {
            return await _db.Books.Include(b => b.Genre).Include(b => b.Category).Include(b => b.Series).Include(b => b.Author).Include(b => b.Reviews).ToListAsync();
        }
        /// <summary>
        /// Возвращает кортеж из кода результата и результата
        /// </summary>
        /// <param name="_db">Контекст базы данных</param>
        /// <param name="id">Id Книги</param>
        /// <returns>Возвращает 1 если книги с таким id нету, возвращает 0 если книга найдена</returns>
        static public async Task<(int, Book?)> ReturnBookByIdAsync(DatabaseContext _db, int id)
        {
            var book = await _db.Books.Include(b => b.Genre).Include(b => b.Category).Include(b => b.Series).Include(b => b.Author).Include(b => b.Reviews).Where(b => b.Id == id).FirstOrDefaultAsync();
            if (book == null)
            {
                return (1, book);
            }
            return (0, book);
        }
        /// <summary>
        /// Удаляет книгу по указанному id
        /// </summary>
        /// <param name="_db">Контекст базы данных</param>
        /// <param name="id">Айди книги</param>
        /// <returns>Возвращает 1 если книга не найдена
        /// Возвращает 0 если удаление прошло успешно</returns>
        static public async Task<int> DeleteByIdAsync(DatabaseContext _db, int id)
        {
            var book1 = await _db.Books.FindAsync(id);
            if (book1 == null)
            {
                return 1;
            }
            _db.Books.Remove(book1);
            await _db.SaveChangesAsync();
            return 0;
        }
        public static async Task<int> UpdateByIDAsync(DatabaseContext _db, int id, int GenreId, int CategoryId, int AuthorId, int SeriesId, BookWithoutExternal WithoutExternalBook)
        {
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
            book.GenreId = GenreId;
            book.CategoryId = CategoryId;
            book.AuthorId = AuthorId;
            book.SeriesId = SeriesId;
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
