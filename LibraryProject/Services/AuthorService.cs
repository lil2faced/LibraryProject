using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class AuthorService
    {
        /// <summary>
        /// Создает автора в базе данных
        /// </summary>
        /// <param name="author">Полное имя автора</param>
        /// <param name="_db">Контекст базы данных</param>
        /// <returns>Возвращает 0 если создание прошло успешно; Возвращает 1 если автор с таким именем уже существует</returns>
        public static async Task<int> AddAuthorAsync(BookAuthor author, DatabaseContext _db)
        {
            var Author = await _db.BookAuthors.Where(a => a.Name == author.Name).FirstOrDefaultAsync();
            if (Author != null)
            {
                return 1;
            }
            _db.BookAuthors.Add(author);
            await _db.SaveChangesAsync();
            return 0;
        }
        /// <summary>
        /// Возвращает список всех авторов из базы данных
        /// </summary>
        /// <param name="_db">Контекст базы данных</param>
        public static async Task<List<BookAuthor>> GetAllAuthorsAsync(DatabaseContext _db)
        {
            return await _db.BookAuthors.ToListAsync();
        }
        /// <summary>
        /// Возвращает кортеж из кода результата и автора(при его отсутствии возвращает 1)
        /// </summary>
        /// <param name="id">id автора</param>
        /// <param name="_db">Контекст базы данных</param>
        /// <returns>Возвращает 1 если автор не был найден; Возвращает 0 если ошибок не было </returns>
        public static async Task<(int, BookAuthor?)> GetAuthorByIdAsync(int id, DatabaseContext _db)
        {
            var author = await _db.BookAuthors.FindAsync(id);
            if (author == null)
            {
                return (1, author);
            }
            return (0, author);
        }
        public static async Task<int> DeleteByIdAsync(int id, DatabaseContext _db)
        {
            var author = await _db.BookAuthors.FindAsync(id);
            if (author == null)
            {
                return 1;
            }
            _db.BookAuthors.Remove(author);
            await _db.SaveChangesAsync();
            return 0;
        }
        public static async Task<int> UpdateByIDAsync(DatabaseContext _db, int id, BookAuthor aut)
        {
            var author = await _db.BookAuthors.FindAsync(id);
            if (author == null)
            {
                return 1;
            }
            author.Name = aut.Name;
            author.Biography = aut.Biography;
            _db.BookAuthors.Update(author);
            await _db.SaveChangesAsync();
            return 0;
        }
    }
}
