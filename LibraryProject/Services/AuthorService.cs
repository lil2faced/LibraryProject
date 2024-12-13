using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DatabaseContext _db;
        public AuthorService(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task<int> AddAuthorAsync(BookAuthor author)
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

        public async Task<List<BookAuthor>> GetAllAuthorsAsync()
        {
            return await _db.BookAuthors.ToListAsync();
        }

        public async Task<(int, BookAuthor?)> GetAuthorByIdAsync(int id)
        {
            var author = await _db.BookAuthors.FindAsync(id);
            if (author == null)
            {
                return (1, author);
            }
            return (0, author);
        }

        public async Task<int> DeleteByIdAsync(int id)
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

        public async Task<int> UpdateByIDAsync(int id, BookAuthor aut)
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
